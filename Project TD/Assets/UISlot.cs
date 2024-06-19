using Rito;
using Rito.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    /***********************************************************************
        *                               Option Fields
        ***********************************************************************/
    #region .
    [Header("Options")]
    [Range(0, 10)]
    [SerializeField] protected int _horizontalSlotCount = 6;  // 슬롯 가로 개수
    [Range(0, 10)]
    [SerializeField] protected int _verticalSlotCount = 3;      // 슬롯 세로 개수
    [SerializeField] protected float _slotMargin = 8f;          // 한 슬롯의 상하좌우 여백
    [SerializeField] protected float _contentAreaPadding = 20f; // 인벤토리 영역의 내부 여백
    [Range(32, 128)]
    [SerializeField] protected float _slotSize = 128f;      // 각 슬롯의 크기

    [Space]
    [SerializeField] protected bool _showTooltip = true;
    [SerializeField] protected bool _showHighlight = true;
    [SerializeField] protected bool _showRemovingPopup = true;

    [Header("Connected Objects")]
    [SerializeField] protected RectTransform _contentAreaRT; // 슬롯들이 위치할 영역
    [SerializeField] protected GameObject _slotUiPrefab;     // 슬롯의 원본 프리팹
    [SerializeField] protected ItemTooltipUI _itemTooltip;   // 아이템 정보를 보여줄 툴팁 UI
    [SerializeField] protected InventoryPopupUI _popup;      // 팝업 UI 관리 객체

    [Space(16)]
    [SerializeField] protected bool _mouseReversed = false; // 마우스 클릭 반전 여부

    [SerializeField] protected GameObject _grPopup;
    #endregion

    /***********************************************************************
        *                               Private Fields
        ***********************************************************************/
    #region .

    protected List<ItemSlotUI> _slotUIList = new List<ItemSlotUI>();
    protected GraphicRaycaster _gr;
    protected PointerEventData _ped;
    protected List<RaycastResult> _rrList;

    protected ItemSlotUI _pointerOverSlot; // 현재 포인터가 위치한 곳의 슬롯
    protected ItemSlotUI _beginDragSlot; // 현재 드래그를 시작한 슬롯
    protected Transform _beginDragIconTransform; // 해당 슬롯의 아이콘 트랜스폼

    protected int _leftClick = 0;
    protected int _rightClick = 1;

    protected Vector3 _beginDragIconPoint;   // 드래그 시작 시 슬롯의 위치
    protected Vector3 _beginDragCursorPoint; // 드래그 시작 시 커서의 위치
    protected int _beginDragSlotSiblingIndex;

    private EventSystem customEventSystem;

    #endregion
    /***********************************************************************
        *                               Unity Events
        ***********************************************************************/
    #region .
    protected virtual void Start()
    {
        Init();
        InitSlots();
    }
    #endregion
    /***********************************************************************
        *                               Init Methods
        ***********************************************************************/
    private void Init()
    {
        TryGetComponent(out _gr);
        if (_gr == null)
            _gr = gameObject.AddComponent<GraphicRaycaster>();

        // Graphic Raycaster
        customEventSystem = _grPopup.GetComponent<EventSystem>();
        _ped = new PointerEventData(customEventSystem);
        _rrList = new List<RaycastResult>(10);

        // Item Tooltip UI
        if (_itemTooltip == null)
        {
            _itemTooltip = GetComponentInChildren<ItemTooltipUI>();
            EditorLog("인스펙터에서 아이템 툴팁 UI를 직접 지정하지 않아 자식에서 발견하여 초기화하였습니다.");
        }
    }

    protected virtual void InitSlots()
    {
        // 슬롯 프리팹 설정
        _slotUiPrefab.TryGetComponent(out RectTransform slotRect);
        slotRect.sizeDelta = new Vector2(_slotSize, _slotSize);

        _slotUiPrefab.TryGetComponent(out ItemSlotUI itemSlot);
        if (itemSlot == null)
            _slotUiPrefab.AddComponent<ItemSlotUI>();

        _slotUIList = new List<ItemSlotUI>(_verticalSlotCount * _horizontalSlotCount);
    }

    /***********************************************************************
        *                               Mouse Event Methods
        ***********************************************************************/
    #region .
    


    #endregion
    /***********************************************************************
        *                               Private Methods
        ***********************************************************************/
    #region .
    /// <summary> UI 및 인벤토리에서 아이템 제거 </summary>
    protected void TryRemoveItem(int index)
    {
        EditorLog($"UI - Try Remove Item : Slot [{index}]");

        Inventory.Instance.Remove(index);
    }

    /// <summary> 두 슬롯의 아이템 교환 </summary>
    protected void TrySwapItems(ItemSlotUI from, ItemSlotUI to)
    {
        if (from == to)
        {
            EditorLog($"UI - Try Swap Items: Same Slot [{from.Index}]");
            return;
        }

        EditorLog($"UI - Try Swap Items: Slot [{from.Index} -> {to.Index}]");

        from.SwapOrMoveIcon(to);
        Inventory.Instance.Swap(from.Index, to.Index);
    }

    /// <summary> 툴팁 UI의 슬롯 데이터 갱신 </summary>
    protected void UpdateTooltipUI(ItemSlotUI slot)
    {
        if (!slot.IsAccessible || !slot.HasItem)
            return;

        // 툴팁 정보 갱신
        _itemTooltip.SetItemInfo(slot.item);

        // 툴팁 위치 조정
        _itemTooltip.SetRectPosition(slot.SlotRect);
    }
    #endregion

    /***********************************************************************
        *                               Public Methods
        ***********************************************************************/
    #region .

    /// <summary> 마우스 클릭 좌우 반전시키기 (true : 반전) </summary>
    public void InvertMouse(bool value)
    {
        _leftClick = value ? 1 : 0;
        _rightClick = value ? 0 : 1;

        _mouseReversed = value;
    }

    /// <summary> 슬롯에 아이템 아이콘 등록 </summary>
    public void SetItemIcon(int index, Sprite icon)
    {
        EditorLog($"Set Item Icon : Slot [{index}]");

        _slotUIList[index].SetItem(icon);
    }

    public void SetItem(int index, Item item)
    {
        _slotUIList[index].item = item.Data;
    }


    /// <summary> 해당 슬롯의 아이템 개수 텍스트 지정 </summary>
    public void SetItemAmountText(int index, int amount)
    {
        EditorLog($"Set Item Amount Text : Slot [{index}], Amount [{amount}]");

        // NOTE : amount가 1 이하일 경우 텍스트 미표시
        _slotUIList[index].SetItemAmount(amount);
    }

    /// <summary> 해당 슬롯의 아이템 개수 텍스트 지정 </summary>
    public void HideItemAmountText(int index)
    {
        EditorLog($"Hide Item Amount Text : Slot [{index}]");

        _slotUIList[index].SetItemAmount(1);
    }

    /// <summary> 슬롯에서 아이템 아이콘 제거, 개수 텍스트 숨기기 </summary>
    public void RemoveItem(int index)
    {
        EditorLog($"Remove Item : Slot [{index}]");

        _slotUIList[index].RemoveItem();
    }

    /// <summary> 접근 가능한 슬롯 범위 설정 </summary>
    public void SetAccessibleSlotRange(int accessibleSlotCount)
    {
        for (int i = 0; i < _slotUIList.Count; i++)
        {
            _slotUIList[i].SetSlotAccessibleState(i < accessibleSlotCount);
        }
    }

    #endregion

    /***********************************************************************
        *                               Editor Only Debug
        ***********************************************************************/
    #region .
#if UNITY_EDITOR
    [Header("Editor Options")]
    [SerializeField] private bool _showDebug = true;
#endif
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    protected void EditorLog(object message)
    {
        if (!_showDebug) return;
        UnityEngine.Debug.Log($"[InventoryUI] {message}");
    }

    #endregion
    /***********************************************************************
       *                               Editor Preview
       ***********************************************************************/
    #region .
#if UNITY_EDITOR
    [SerializeField] private bool __showPreview = false;

    [Range(0.01f, 1f)]
    [SerializeField] private float __previewAlpha = 0.1f;

    private List<GameObject> __previewSlotGoList = new List<GameObject>();
    private int __prevSlotCountPerLine;
    private int __prevSlotLineCount;
    private float __prevSlotSize;
    private float __prevSlotMargin;
    private float __prevContentPadding;
    private float __prevAlpha;
    private bool __prevShow = false;
    private bool __prevMouseReversed = false;

    private void OnValidate()
    {
        if (__prevMouseReversed != _mouseReversed)
        {
            __prevMouseReversed = _mouseReversed;
            InvertMouse(_mouseReversed);

            EditorLog($"Mouse Reversed : {_mouseReversed}");
        }

        if (Application.isPlaying) return;

        if (__showPreview && !__prevShow)
        {
            CreateSlots();
        }
        __prevShow = __showPreview;

        if (Unavailable())
        {
            ClearAll();
            return;
        }
        if (CountChanged())
        {
            ClearAll();
            CreateSlots();
            __prevSlotCountPerLine = _horizontalSlotCount;
            __prevSlotLineCount = _verticalSlotCount;
        }
        if (ValueChanged())
        {
            DrawGrid();
            __prevSlotSize = _slotSize;
            __prevSlotMargin = _slotMargin;
            __prevContentPadding = _contentAreaPadding;
        }
        if (AlphaChanged())
        {
            SetImageAlpha();
            __prevAlpha = __previewAlpha;
        }

        bool Unavailable()
        {
            return !__showPreview ||
                    _horizontalSlotCount < 1 ||
                    _verticalSlotCount < 1 ||
                    _slotSize <= 0f ||
                    _contentAreaRT == null ||
                    _slotUiPrefab == null;
        }
        bool CountChanged()
        {
            return _horizontalSlotCount != __prevSlotCountPerLine ||
                   _verticalSlotCount != __prevSlotLineCount;
        }
        bool ValueChanged()
        {
            return _slotSize != __prevSlotSize ||
                   _slotMargin != __prevSlotMargin ||
                   _contentAreaPadding != __prevContentPadding;
        }
        bool AlphaChanged()
        {
            return __previewAlpha != __prevAlpha;
        }
        void ClearAll()
        {
            foreach (var go in __previewSlotGoList)
            {
                Destroyer.Destroy(go);
            }
            __previewSlotGoList.Clear();
        }
        void CreateSlots()
        {
            int count = _horizontalSlotCount * _verticalSlotCount;
            __previewSlotGoList.Capacity = count;

            // 슬롯의 피벗은 Left Top으로 고정
            RectTransform slotPrefabRT = _slotUiPrefab.GetComponent<RectTransform>();
            slotPrefabRT.pivot = new Vector2(0f, 1f);

            for (int i = 0; i < count; i++)
            {
                GameObject slotGo = Instantiate(_slotUiPrefab);
                slotGo.transform.SetParent(_contentAreaRT.transform);
                slotGo.SetActive(true);
                slotGo.AddComponent<PreviewItemSlot>();

                slotGo.transform.localScale = Vector3.one; // 버그 해결

                HideGameObject(slotGo);

                __previewSlotGoList.Add(slotGo);
            }

            DrawGrid();
            SetImageAlpha();
        }
        void DrawGrid()
        {
            Vector2 beginPos = new Vector2(_contentAreaPadding, -_contentAreaPadding);
            Vector2 curPos = beginPos;

            // Draw Slots
            int index = 0;
            for (int j = 0; j < _verticalSlotCount; j++)
            {
                for (int i = 0; i < _horizontalSlotCount; i++)
                {
                    GameObject slotGo = __previewSlotGoList[index++];
                    RectTransform slotRT = slotGo.GetComponent<RectTransform>();

                    slotRT.anchoredPosition = curPos;
                    slotRT.sizeDelta = new Vector2(_slotSize, _slotSize);
                    __previewSlotGoList.Add(slotGo);

                    // Next X
                    curPos.x += (_slotMargin + _slotSize);
                }

                // Next Line
                curPos.x = beginPos.x;
                curPos.y -= (_slotMargin + _slotSize);
            }
        }
        void HideGameObject(GameObject go)
        {
            go.hideFlags = HideFlags.HideAndDontSave;

            Transform tr = go.transform;
            for (int i = 0; i < tr.childCount; i++)
            {
                tr.GetChild(i).gameObject.hideFlags = HideFlags.HideAndDontSave;
            }
        }
        void SetImageAlpha()
        {
            foreach (var go in __previewSlotGoList)
            {
                var images = go.GetComponentsInChildren<Image>();
                foreach (var img in images)
                {
                    img.color = new Color(img.color.r, img.color.g, img.color.b, __previewAlpha);
                    var outline = img.GetComponent<Outline>();
                    if (outline)
                        outline.effectColor = new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, __previewAlpha);
                }
            }
        }
    }

    private class PreviewItemSlot : MonoBehaviour { }

    [UnityEditor.InitializeOnLoad]
    private static class Destroyer
    {
        private static Queue<GameObject> targetQueue = new Queue<GameObject>();

        static Destroyer()
        {
            UnityEditor.EditorApplication.update += () =>
            {
                for (int i = 0; targetQueue.Count > 0 && i < 100000; i++)
                {
                    var next = targetQueue.Dequeue();
                    DestroyImmediate(next);
                }
            };
        }
        public static void Destroy(GameObject go) => targetQueue.Enqueue(go);
    }
#endif

    #endregion
}
