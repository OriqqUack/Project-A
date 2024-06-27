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
        [SerializeField] private CollectionTooltipUI _collectionTooltip;   // ���� ������ ������ ���� UI

        [Space(16)]
        [SerializeField] private bool _mouseReversed = false; // ���콺 Ŭ�� ���� ����

        /// <summary> ����� ���� </summary>
        private CollectionSystem _collectionSystem;

        private List<CollectionSlotUI> _slotUIList = new List<CollectionSlotUI>();
        private GraphicRaycaster _gr;
        private PointerEventData _ped;
        private List<RaycastResult> _rrList;

        private CollectionSlotUI _pointerOverSlot; // ���� �����Ͱ� ��ġ�� ���� ����
        private CollectionSlotUI _beginDragSlot; // ���� �巡�׸� ������ ����
        private Transform _beginDragIconTransform; // �ش� ������ ������ Ʈ������

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
                EditorLog("�ν����Ϳ��� ������ ���� UI�� ���� �������� �ʾ� �ڽĿ��� �߰��Ͽ� �ʱ�ȭ�Ͽ����ϴ�.");
            }
        }

        private bool IsOverUI()
            => EventSystem.current.IsPointerOverGameObject();

        /// <summary> ����ĳ��Ʈ�Ͽ� ���� ù ��° UI���� ������Ʈ ã�� ���� </summary>
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

        /// <summary> ���� ���� ���� �����ְų� ���߱� </summary>
        private void ShowOrHideItemTooltip()
        {
            // ���콺�� ��ȿ�� ���� ������ ���� �ö�� �ִٸ� ���� �����ֱ�
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

        /// <summary> ���Կ� Ŭ���ϴ� ��� </summary>
        private void OnPointerDown()
        {
            // Right Click : Use Item
            if (Input.GetMouseButtonDown(_rightClick))
            {
                CollectionSlotUI slot = RaycastAndGetFirstComponent<CollectionSlotUI>();

            }
        }

        /// <summary> ���� UI�� ���� ������ ���� </summary>
        private void UpdateTooltipUI(CollectionSlotUI slot)
        {
            if (!slot.IsAccessible)
                return;

            // ���� ���� ����
            _collectionTooltip.SetItemInfo(_collectionSystem.GetItemData(slot.Index));

            // ���� ��ġ ����
            _collectionTooltip.SetRectPosition(slot.SlotRect);
        }


        /// <summary> ���� ���� ��� (�������� ���� ȣ��) </summary>
        public void SetCollectionReference(CollectionSystem collectionSystem)
        {
            _collectionSystem = collectionSystem;
        }

        /// <summary> ���콺 Ŭ�� �¿� ������Ű�� (true : ����) </summary>
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

