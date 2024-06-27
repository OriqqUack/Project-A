using Rito.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Minseok.CollectionSystem
{
    public class CollectionSystemUI : MonoBehaviour
    {
        [Space]
        [SerializeField] private bool _showTooltip = true;

        [Header("Connected Objects")]
        [SerializeField] private CollectionTooltipUI _collectionTooltip;   // 도감 정보를 보여줄 툴팁 UI

        [Space(16)]
        [SerializeField] private bool _mouseReversed = false; // 마우스 클릭 반전 여부

        /// <summary> 연결된 도감 </summary>
        private CollectionSystem _collectionSystem;

        private List<CollectionSlotUI> _slotUIList = new List<CollectionSlotUI>();
        private GraphicRaycaster _gr;
        private PointerEventData _ped;
        private List<RaycastResult> _rrList;

        private CollectionSlotUI _pointerOverSlot; // 현재 포인터가 위치한 곳의 슬롯
        private CollectionSlotUI _beginDragSlot; // 현재 드래그를 시작한 슬롯
        private Transform _beginDragIconTransform; // 해당 슬롯의 아이콘 트랜스폼

        private int _leftClick = 0;
        private int _rightClick = 1;


        private void Start()
        {
            Init();
            //InitButtonEvents();
        }

        private void Update()
        {
            _ped.position = Input.mousePosition;

            OnPointerEnterAndExit();
            if (_showTooltip) ShowOrHideItemTooltip();
            OnPointerDown();
        }

        private void Init()
        {
            TryGetComponent(out _gr);
            if (_gr == null)
                _gr = gameObject.AddComponent<GraphicRaycaster>();

            // Graphic Raycaster
            _ped = new PointerEventData(EventSystem.current);
            _rrList = new List<RaycastResult>(10);

            // Item Tooltip UI
            if (_collectionTooltip == null)
            {
                _collectionTooltip = GetComponentInChildren<CollectionTooltipUI>();
                EditorLog("인스펙터에서 아이템 툴팁 UI를 직접 지정하지 않아 자식에서 발견하여 초기화하였습니다.");
            }
        }

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
            _pointerOverSlot = RaycastAndGetFirstComponent<CollectionSlotUI>();

            // ===================== Local Methods ===============================
        }

        /// <summary> 도감 정보 툴팁 보여주거나 감추기 </summary>
        private void ShowOrHideItemTooltip()
        {
            // 마우스가 유효한 도감 아이콘 위에 올라와 있다면 툴팁 보여주기
            bool isValid =
                _pointerOverSlot != null && _pointerOverSlot.IsAccessible;

            if (isValid)
            {
                UpdateTooltipUI(_pointerOverSlot);
                _collectionTooltip.Show();
            }
            else
                _collectionTooltip.Hide();
        }

        /// <summary> 슬롯에 클릭하는 경우 </summary>
        private void OnPointerDown()
        {
            // Right Click : Use Item
            if (Input.GetMouseButtonDown(_rightClick))
            {
                CollectionSlotUI slot = RaycastAndGetFirstComponent<CollectionSlotUI>();

            }
        }

        /// <summary> 툴팁 UI의 슬롯 데이터 갱신 </summary>
        private void UpdateTooltipUI(CollectionSlotUI slot)
        {
            if (!slot.IsAccessible)
                return;

            // 툴팁 정보 갱신
            _collectionTooltip.SetItemInfo(_collectionSystem.GetItemData(slot.Index));

            // 툴팁 위치 조정
            _collectionTooltip.SetRectPosition(slot.SlotRect);
        }


        /// <summary> 도감 참조 등록 (도감에서 직접 호출) </summary>
        public void SetCollectionReference(CollectionSystem collectionSystem)
        {
            _collectionSystem = collectionSystem;
        }

        /// <summary> 마우스 클릭 좌우 반전시키기 (true : 반전) </summary>
        public void InvertMouse(bool value)
        {
            _leftClick = value ? 1 : 0;
            _rightClick = value ? 0 : 1;

            _mouseReversed = value;
        }

#if UNITY_EDITOR
        [Header("Editor Options")]
        [SerializeField] private bool _showDebug = true;
#endif
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        private void EditorLog(object message)
        {
            if (!_showDebug) return;
            UnityEngine.Debug.Log($"[CollectionUI] {message}");
        }
    }
}

