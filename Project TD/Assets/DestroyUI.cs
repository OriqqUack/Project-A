using Rito.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DestroyUI : UISlot
{
    /***********************************************************************
        *                               Option Fields
        ***********************************************************************/
    [Header("Buttons")]
    [SerializeField] private Button _destroyButton;

    [Header("Connected Objects")]
    [SerializeField] protected RectTransform _beforeContents; // 분해전 아이템 슬롯위치
    [SerializeField] protected RectTransform _afterContents; // 분해전 아이템 슬롯위치

    [SerializeField]
    private List<ItemData> normalItemDecomposer;
    [SerializeField]
    private List<ItemData> rearItemDecomposer;
    [SerializeField]
    private List<ItemData> epicItemDecomposer;
    [SerializeField]
    private List<ItemData> legendaryItemDecomposer;

    /***********************************************************************
        *                               Private Fields
        ***********************************************************************/
    private int _slotIndex = 100;

    private Dictionary<Define.ItemGrade, List<ItemData>> gradeToMaterials;
    protected List<ItemSlotUI> _afterSlotUIList = new List<ItemSlotUI>();

    /***********************************************************************
        *                               Unity Events
        ***********************************************************************/
    #region .
    protected override void Start()
    {
        base.Start();
        Inventory.Instance.ConnectDestroyUI(this);
        InitList();
        InitButtonEvents();
        StartCoroutine(DisableLayoutGroupAfterFrame());
    }

    private void Update()
    {
        _ped.position = Input.mousePosition;

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
    protected override void InitSlots()
    {
        base.InitSlots();

        //int index = 0;

        // 슬롯들 동적 생성
        MakingSlot(_beforeContents);
        MakingSlot(_afterContents);
    }

    private void MakingSlot(RectTransform contents)
    {
        for (int j = 0; j < _verticalSlotCount; j++)
        {
            for (int i = 0; i < _horizontalSlotCount; i++)
            {
                int slotIndex = (_horizontalSlotCount * j) + i;

                var slotRT = CloneSlot(contents);
                slotRT.gameObject.SetActive(true);
                slotRT.gameObject.name = $"Item Slot [{slotIndex}]";

                var slotUI = slotRT.GetComponent<ItemSlotUI>();
                //slotUI.item = Inventory.Instance._items[index]?.Data;
                //index++;
                slotUI.SetSlotIndex(slotIndex + _slotIndex);

                if (slotRT.transform.parent.name == "BeforeDestroy")
                    _slotUIList.Add(slotUI);
                else
                    _afterSlotUIList.Add(slotUI);
            }
        }
        // 슬롯 프리팹 - 프리팹이 아닌 경우 파괴
        if (_slotUiPrefab.scene.rootCount != 0)
            Destroy(_slotUiPrefab);

        // -- Local Method --
        RectTransform CloneSlot(RectTransform contentsArea)
        {
            GameObject slotGo = Instantiate(_slotUiPrefab);
            RectTransform rt = slotGo.GetComponent<RectTransform>();
            rt.SetParent(contentsArea);

            return rt;
        }
    }

    private void InitButtonEvents()
    {
        _destroyButton.onClick.AddListener(() => OnClickedDestroyBtn());
    }

    private void InitList()
    {
        gradeToMaterials = new Dictionary<Define.ItemGrade, List<ItemData>>();

        gradeToMaterials[Define.ItemGrade.Normal] = normalItemDecomposer;
        gradeToMaterials[Define.ItemGrade.Rear] = rearItemDecomposer;
        gradeToMaterials[Define.ItemGrade.Epic] = epicItemDecomposer;
        gradeToMaterials[Define.ItemGrade.Legendary] = legendaryItemDecomposer;
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
                _beginDragSlot.transform.SetAsLastSibling();

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
            //인벤토리로 돌아가기
        }
    }
    /// <summary> 드래그하는 도중 </summary>
    private void OnPointerDrag()
    {
        if (_beginDragSlot == null) return;

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
        ItemSlotUI endDragSlot = RaycastAndGetFirstComponent<ItemSlotUI>();

        // 아이템 슬롯끼리 아이콘 교환 또는 이동
        if (endDragSlot != null && endDragSlot.IsAccessible)
        {            TrySwapItems(_beginDragSlot, endDragSlot);

            // 툴팁 갱신
            UpdateTooltipUI(endDragSlot);
            return;
        }

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
    }

    private int ListEmptyRemain()
    {
        int i = 0;
        foreach (var itemSlot in _afterSlotUIList)
        {
            if (itemSlot.item == null)
                i++;
        }
        return i;
    }

    private void RemoveAfterDestroy(int index)
    {
        _afterSlotUIList[index].RemoveItem();
    }
    #endregion

    /***********************************************************************
        *                               Private Methods
        ***********************************************************************/
    #region .

    private IEnumerator DisableLayoutGroupAfterFrame()
    {
        HorizontalLayoutGroup beforeGridLayout = _beforeContents.GetComponent<HorizontalLayoutGroup>();
        HorizontalLayoutGroup afterGridLayout = _afterContents.GetComponent<HorizontalLayoutGroup>();
        // 한 프레임 대기
        yield return null;
        if (beforeGridLayout != null && afterGridLayout != null)
        {
            beforeGridLayout.enabled = false;
            afterGridLayout.enabled = false;
        }
    }
    #endregion

    /***********************************************************************
        *                               Public Methods
        ***********************************************************************/
    public ItemSlotUI GetItemSlotUI(int idx)
    {
        return _slotUIList[idx];
    }

    public int GetItemSlotUIListCount()
    {
        return _slotUIList.Count;
    }

    public void OnClickedDestroyBtn()
    {
        //인벤토리 남은 슬롯이 분해완료 슬롯보다 적을 시 판단
        if (Inventory.Instance.GetEmptySlotCount() - (_afterSlotUIList.Count - ListEmptyRemain()) < 0)
        {
            Debug.Log("인벤토리가 꽉 찼어용!");
            return;
        }

        if (_afterSlotUIList.Count == ListEmptyRemain())
            return;

        int index = 0;
        foreach(var itemSlot in _afterSlotUIList)
        {
            if (itemSlot.item == null)
                continue;
            if(itemSlot.item is CountableItemData)
                Inventory.Instance.Add(itemSlot.item, itemSlot.GetItemAmount());
            else
                Inventory.Instance.Add(itemSlot.item);

            RemoveItem(index);
            RemoveAfterDestroy(index);
            index++;
        }

        Inventory.Instance._destroyIndex = 0;
    }
    
    /// <summary> 아이템을 추가할때마다 그레이들 별로 after리스트에 결과물을 보여줌 </summary>
    public bool UpdateAfterDestroy(ItemSlotUI itemSlotUI)
    {
        if (gradeToMaterials.ContainsKey(itemSlotUI.item.itemGrade))
        {
            //분해 갯수가 넘을것 같으면 false반환
            if (gradeToMaterials[itemSlotUI.item.itemGrade].Count > ListEmptyRemain())
                return false;

            foreach (var resultItem in gradeToMaterials[itemSlotUI.item.itemGrade])
            {
                if (ListEmptyRemain() == 0)
                    break;

                foreach(var itemSlot in _afterSlotUIList)
                {
                    if(itemSlot.item == null)
                    {
                        itemSlot.item = resultItem;
                        itemSlot.SetItem(resultItem.IconSprite);
                        if(itemSlot.item is CountableItemData)
                        {
                            itemSlot.SetItemAmount(1);
                        }
                        break;
                    }

                    if(itemSlot.item == resultItem)
                    {
                        if (itemSlot.item is CountableItemData)
                        {
                            int currAmount = itemSlot.GetItemAmount();
                            itemSlot.SetItemAmount(currAmount + 1);
                            break;
                        }
                    }
                }
            }
        }
        return true;
    }
}
