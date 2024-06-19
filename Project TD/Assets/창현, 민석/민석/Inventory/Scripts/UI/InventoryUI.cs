using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
    [기능 - 에디터 전용]
    - 게임 시작 시 동적으로 생성될 슬롯 미리보기(개수, 크기 미리보기 가능)

    [기능 - 유저 인터페이스]
    - 슬롯에 마우스 올리기
      - 사용 가능 슬롯 : 하이라이트 이미지 표시
      - 아이템 존재 슬롯 : 아이템 정보 툴팁 표시

    - 드래그 앤 드롭
      - 아이템 존재 슬롯 -> 아이템 존재 슬롯 : 두 아이템 위치 교환
      - 아이템 존재 슬롯 -> 아이템 미존재 슬롯 : 아이템 위치 변경
        - Shift 또는 Ctrl 누른 상태일 경우 : 셀 수 있는 아이템 수량 나누기
      - 아이템 존재 슬롯 -> UI 바깥 : 아이템 버리기

    - 슬롯 우클릭
      - 사용 가능한 아이템일 경우 : 아이템 사용

    - 기능 버튼(좌측 상단)
      - Trim : 앞에서부터 빈 칸 없이 아이템 채우기
      - Sort : 정해진 가중치대로 아이템 정렬

    - 필터 버튼(우측 상단)
      - [A] : 모든 아이템 필터링
      - [E] : 장비 아이템 필터링
      - [P] : 소비 아이템 필터링

      * 필터링에서 제외된 아이템 슬롯들은 조작 불가

    [기능 - 기타]
    - InvertMouse(bool) : 마우스 좌클릭/우클릭 반전 여부 설정
*/

// 날짜 : 2021-03-07 PM 7:34:31
// 작성자 : Rito

namespace Rito.InventorySystem
{
    public class InventoryUI : UISlot
    {
        enum InventroyState
        {
            None,
            Inventory,
            CraftingAndDestroy
        }
        /***********************************************************************
        *                               Option Fields
        ***********************************************************************/
        [Header("Buttons")]
        [SerializeField] private Button _trimButton;
        [SerializeField] private Button _sortButton;

        [Header("Filter Toggles")]
        [SerializeField] protected Toggle _toggleFilterAll;
        [SerializeField] protected Toggle _toggleFilterEquipments;
        [SerializeField] protected Toggle _toggleFilterPortions;

        /***********************************************************************
        *                               Private Fields
        ***********************************************************************/
        /// <summary> 인벤토리 UI 내 아이템 필터링 옵션 </summary>
        private enum FilterOption
        {
            All, Equipment, Portion
        }
        private FilterOption _currentFilterOption = FilterOption.All;
        private InventroyState _inventoryState = InventroyState.None;

        /***********************************************************************
        *                               Unity Events
        ***********************************************************************/
        #region .
        private void Awake()
        {
        }
        protected override void Start()
        {
            base.Start();
            Inventory.Instance.ConnectInventoryUI(this);
            InitButtonEvents();
            InitToggleEvents();
            StartCoroutine(DisableGridLayoutGroupAfterFrame());
            InitState();
        }

        private void Update()
        {
            _ped.position = Input.mousePosition;

            ItemSlotUI item = RaycastAndGetFirstComponent<ItemSlotUI>();
            Debug.Log(item);

            OnPointerEnterAndExit();
            if (_showTooltip) ShowOrHideItemTooltip();
            OnPointerDown();
            OnPointerDrag();
            OnPointerUp();
        }
        #endregion
        /***********************************************************************
        *                               Init Methods
        ***********************************************************************/
        #region .

        /// <summary> 지정된 개수만큼 슬롯 영역 내에 슬롯들 동적 생성 </summary>
        protected override void InitSlots()
        {
            base.InitSlots();


            // 슬롯들 동적 생성
            for (int j = 0; j < _verticalSlotCount; j++)
            {
                for (int i = 0; i < _horizontalSlotCount; i++)
                {
                    int slotIndex = (_horizontalSlotCount * j) + i;

                    var slotRT = CloneSlot();
                    //slotRT.pivot = new Vector2(0f, 1f); // Left Top
                    //slotRT.anchoredPosition = curPos;
                    slotRT.gameObject.SetActive(true);
                    slotRT.gameObject.name = $"Item Slot [{slotIndex}]";

                    var slotUI = slotRT.GetComponent<ItemSlotUI>();
                    slotUI.item = Inventory.Instance._items[slotIndex]?.Data;
                    slotUI.SetSlotIndex(slotIndex);
                    _slotUIList.Add(slotUI);
                }
            }

            // 슬롯 프리팹 - 프리팹이 아닌 경우 파괴
            if(_slotUiPrefab.scene.rootCount != 0)
                Destroy(_slotUiPrefab);

            
            // -- Local Method --
            RectTransform CloneSlot()
            {
                GameObject slotGo = Instantiate(_slotUiPrefab);
                RectTransform rt = slotGo.GetComponent<RectTransform>();
                rt.SetParent(_contentAreaRT);

                return rt;
            }
        }

        private void InitButtonEvents()
        {
            _trimButton.onClick.AddListener(() => Inventory.Instance.TrimAll());
            _sortButton.onClick.AddListener(() => Inventory.Instance.SortAll());
        }

        private void InitToggleEvents()
        {
            _toggleFilterAll.onValueChanged.AddListener(       flag => UpdateFilter(flag, FilterOption.All));
            _toggleFilterEquipments.onValueChanged.AddListener(flag => UpdateFilter(flag, FilterOption.Equipment));
            _toggleFilterPortions.onValueChanged.AddListener(  flag => UpdateFilter(flag, FilterOption.Portion));

            // Local Method
            void UpdateFilter(bool flag, FilterOption option)
            {
                if (flag)
                {
                    _currentFilterOption = option;
                    UpdateAllSlotFilters();
                }
            }
        }

        private void InitState()
        {
            Transform root = this.transform.parent;
            switch (root.name)
            {
                case "Inventory":
                    _inventoryState = InventroyState.Inventory;
                    break;
                case "CraftingDestroy":
                    _inventoryState = InventroyState.CraftingAndDestroy;
                    break;
            }
        }

        #endregion
        /***********************************************************************
        *                               Mouse Event Methods
        ***********************************************************************/
        #region .

        private bool IsOverUI()
           => EventSystem.current.IsPointerOverGameObject();

        /// <summary> 레이캐스트하여 얻은 첫 번째 UI에서 컴포넌트 찾아 리턴 </summary>
        private T RaycastAndGetFirstComponent<T>() where T : Component
        {
            _rrList.Clear();

            _gr.Raycast(_ped, _rrList);

            if (_rrList.Count == 0)
                return null;

            return _rrList[0].gameObject.GetComponent<T>();
        }
        private void OnPointerEnterAndExit()
        {
            // 이전 프레임의 슬롯
            var prevSlot = _pointerOverSlot;

            // 현재 프레임의 슬롯
            var curSlot = _pointerOverSlot = RaycastAndGetFirstComponent<ItemSlotUI>();

            if (prevSlot == null)
            {
                // Enter
                if (curSlot != null)
                {
                    OnCurrentEnter();
                }
            }
            else
            {
                // Exit
                if (curSlot == null)
                {
                    OnPrevExit();
                }

                // Change
                else if (prevSlot != curSlot)
                {
                    OnPrevExit();
                    OnCurrentEnter();
                }
            }

            // ===================== Local Methods ===============================
            void OnCurrentEnter()
            {
                if (_showHighlight)
                    curSlot.Highlight(true);
            }
            void OnPrevExit()
            {
                prevSlot.Highlight(false);
            }
        }
        /// <summary> 아이템 정보 툴팁 보여주거나 감추기 </summary>
        private void ShowOrHideItemTooltip()
        {
            // 마우스가 유효한 아이템 아이콘 위에 올라와 있다면 툴팁 보여주기
            bool isValid =
                _pointerOverSlot != null && _pointerOverSlot.HasItem && _pointerOverSlot.IsAccessible
                && (_pointerOverSlot != _beginDragSlot); // 드래그 시작한 슬롯이면 보여주지 않기

            if (isValid)
            {
                UpdateTooltipUI(_pointerOverSlot);
                _itemTooltip.Show();
            }
            else
                _itemTooltip.Hide();
        }
        private void OnPointerDown()
        {
            // Left Click : Begin Drag
            if (Input.GetMouseButtonDown(_leftClick))
            {
                _beginDragSlot = RaycastAndGetFirstComponent<ItemSlotUI>();

                // 아이템을 갖고 있는 슬롯만 해당
                if (_beginDragSlot != null && _beginDragSlot.HasItem && _beginDragSlot.IsAccessible)
                {
                    EditorLog($"Drag Begin : Slot [{_beginDragSlot.Index}]");

                    // 위치 기억, 참조 등록
                    _beginDragIconTransform = _beginDragSlot.IconRect.transform;
                    _beginDragIconPoint = _beginDragIconTransform.position;
                    _beginDragCursorPoint = Input.mousePosition;

                    // 맨 위에 보이기
                    _beginDragSlotSiblingIndex = _beginDragSlot.transform.GetSiblingIndex();
                    _beginDragSlot.transform.SetAsFirstSibling();

                    // 해당 슬롯의 하이라이트 이미지를 아이콘보다 뒤에 위치시키기
                    _beginDragSlot.SetHighlightOnTop(false);
                }
                else
                {
                    _beginDragSlot = null;
                }
            }

            if (Input.GetMouseButtonDown(_rightClick))
            {
                ItemSlotUI slot = RaycastAndGetFirstComponent<ItemSlotUI>();

                if (slot.item == null)
                    return;

                if (_inventoryState == InventroyState.Inventory)
                {

                    if (slot != null && slot.HasItem && slot.IsAccessible)
                    {
                        TryUseItem(slot.Index);
                    }
                }
                
                if(_inventoryState == InventroyState.CraftingAndDestroy)
                {
                    Inventory.Instance.InventoryUIToDestoryUI(slot);
                }
            }
        }
        /// <summary> 드래그하는 도중 </summary>
        private void OnPointerDrag()
        {
            if (_beginDragSlot == null) return;

            _beginDragIconTransform.SetParent(_contentAreaRT);

            if (Input.GetMouseButton(_leftClick))
            {
                // 위치 이동
                _beginDragIconTransform.position =
                    _beginDragIconPoint + (Input.mousePosition - _beginDragCursorPoint);
            }
        }
        /// <summary> 클릭을 뗄 경우 </summary>
        void OnPointerUp()
        {
            if (Input.GetMouseButtonUp(_leftClick))
            {
                // End Drag
                if (_beginDragSlot != null)
                {
                    // 위치 복원
                    _beginDragIconTransform.SetParent(_beginDragSlot.transform);
                    _beginDragIconTransform.position = _beginDragIconPoint;

                    // UI 순서 복원
                    _beginDragSlot.transform.SetSiblingIndex(_beginDragSlotSiblingIndex);

                    // 드래그 완료 처리
                    EndDrag();

                    // 해당 슬롯의 하이라이트 이미지를 아이콘보다 앞에 위치시키기
                    _beginDragSlot.SetHighlightOnTop(true);

                    // 참조 제거
                    _beginDragSlot = null;
                    _beginDragIconTransform = null;
                }
            }
        }
        private void EndDrag()
        {
            // 버리기(커서가 UI 레이캐스트 타겟 위에 있지 않은 경우)
            if (!IsOverUI())
            {
                // 확인 팝업 띄우고 콜백 위임
                int index = _beginDragSlot.Index;
                string itemName = Inventory.Instance.GetItemName(index);
                int amount = Inventory.Instance.GetCurrentAmount(index);

                // 셀 수 있는 아이템의 경우, 수량 표시
                if (amount > 1)
                    itemName += $" x{amount}";

                if (_showRemovingPopup)
                    _popup.OpenConfirmationPopup(() => TryRemoveItem(index), itemName);
                else
                    TryRemoveItem(index);
            }
            // 슬롯이 아닌 다른 UI 위에 놓은 경우
            else
            {
                EditorLog($"Drag End(Do Nothing)");
            }

            ItemSlotUI endDragSlot = RaycastAndGetFirstComponent<ItemSlotUI>();

            // 아이템 슬롯끼리 아이콘 교환 또는 이동
            if (endDragSlot != null && endDragSlot.IsAccessible)
            {
                Debug.Log(endDragSlot.Index + " : INdex");
                // 수량 나누기 조건
                // 1) 마우스 클릭 떼는 순간 좌측 Ctrl 또는 Shift 키 유지
                // 2) begin : 셀 수 있는 아이템 / end : 비어있는 슬롯
                // 3) begin 아이템의 수량 > 1
                bool isSeparatable =
                    (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftShift)) &&
                    (Inventory.Instance.IsCountableItem(_beginDragSlot.Index) && !Inventory.Instance.HasItem(endDragSlot.Index));

                // true : 수량 나누기, false : 교환 또는 이동
                bool isSeparation = false;
                int currentAmount = 0;

                // 현재 개수 확인
                if (isSeparatable)
                {
                    currentAmount = Inventory.Instance.GetCurrentAmount(_beginDragSlot.Index);
                    if (currentAmount > 1)
                    {
                        isSeparation = true;
                    }
                }

                // 1. 개수 나누기
                if (isSeparation)
                    TrySeparateAmount(_beginDragSlot.Index, endDragSlot.Index, currentAmount);
                // 2. 교환 또는 이동
                else
                    TrySwapItems(_beginDragSlot, endDragSlot);

                // 툴팁 갱신
                UpdateTooltipUI(endDragSlot);
                return;
            }
        }

        #endregion
        /***********************************************************************
        *                               Private Methods
        ***********************************************************************/
        #region .

        private IEnumerator DisableGridLayoutGroupAfterFrame()
        {
            GridLayoutGroup gridLayout = _contentAreaRT.GetComponent<GridLayoutGroup>();
            // 한 프레임 대기
            yield return null;
            if (gridLayout != null)
            {
                gridLayout.enabled = false;
            }
        }

        /// <summary> 아이템 사용 </summary>
        private void TryUseItem(int index)
        {
            EditorLog($"UI - Try Use Item : Slot [{index}]");

            Inventory.Instance.Use(index);
        }

       

        /// <summary> 셀 수 있는 아이템 개수 나누기 </summary>
        private void TrySeparateAmount(int indexA, int indexB, int amount)
        {
            if (indexA == indexB)
            {
                EditorLog($"UI - Try Separate Amount: Same Slot [{indexA}]");
                return;
            }

            EditorLog($"UI - Try Separate Amount: Slot [{indexA} -> {indexB}]");

            string itemName = $"{Inventory.Instance.GetItemName(indexA)} x{amount}";

            _popup.OpenAmountInputPopup(
                amt => Inventory.Instance.SeparateAmount(indexA, indexB, amt),
                amount, itemName
            );
        }

        public void UpdateSlot(int index)
        {
            if (!Inventory.Instance.IsValidIndex(index)) return;

            Item item = Inventory.Instance._items[index];

            // 1. 아이템이 슬롯에 존재하는 경우
            if (item != null)
            {
                // 아이콘 등록
                SetItemIcon(index, item.Data.IconSprite);
                SetItem(index, item);

                // 1-1. 셀 수 있는 아이템
                if (item is CountableItem ci)
                {
                    // 1-1-1. 수량이 0인 경우, 아이템 제거
                    if (ci.IsEmpty)
                    {
                        Inventory.Instance._items[index] = null;
                        RemoveIcon();
                        return;
                    }
                    // 1-1-2. 수량 텍스트 표시
                    else
                    {
                        SetItemAmountText(index, ci.Amount);
                    }
                }
                // 1-2. 셀 수 없는 아이템인 경우 수량 텍스트 제거
                else
                {
                    HideItemAmountText(index);
                }

                // 슬롯 필터 상태 업데이트
                UpdateSlotFilterState(index, item.Data);
            }
            // 2. 빈 슬롯인 경우 : 아이콘 제거
            else
            {
                RemoveIcon();
            }

            // 로컬 : 아이콘 제거하기
            void RemoveIcon()
            {
                RemoveItem(index);
                HideItemAmountText(index); // 수량 텍스트 숨기기
            }
        }
        /// <summary> 해당하는 인덱스의 슬롯들의 상태 및 UI 갱신 </summary>
        public void UpdateSlot(params int[] indices)
        {
            foreach (var i in indices)
            {
                UpdateSlot(i);
            }
        }

        /// <summary> 모든 슬롯들의 상태를 UI에 갱신 </summary>
        public void UpdateAllSlot()
        {
            for (int i = 0; i < Inventory.Instance.Capacity; i++)
            {
                UpdateSlot(i);
            }
        }
        #endregion
        /***********************************************************************
        *                               Public Methods
        ***********************************************************************/
        #region .

        /// <summary> 특정 슬롯의 필터 상태 업데이트 </summary>
        public void UpdateSlotFilterState(int index, ItemData itemData)
        {
            bool isFiltered = true;

            // null인 슬롯은 타입 검사 없이 필터 활성화
            if(itemData != null)
                switch (_currentFilterOption)
                {
                    case FilterOption.Equipment:
                        isFiltered = (itemData is EquipmentItemData);
                        break;

                    case FilterOption.Portion:
                        isFiltered = (itemData is PortionItemData);
                        break;
                }

            _slotUIList[index].SetItemAccessibleState(isFiltered);
        }

        /// <summary> 모든 슬롯 필터 상태 업데이트 </summary>
        public void UpdateAllSlotFilters()
        {
            int capacity = Inventory.Instance.Capacity;

            for (int i = 0; i < capacity; i++)
            {
                ItemData data = Inventory.Instance.GetItemData(i);
                UpdateSlotFilterState(i, data);
            }
        }

        #endregion
    }
}