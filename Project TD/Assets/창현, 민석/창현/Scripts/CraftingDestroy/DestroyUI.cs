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
    [SerializeField] protected RectTransform _beforeContents; // ������ ������ ������ġ
    [SerializeField] protected RectTransform _afterContents; // ������ ������ ������ġ

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

        // ���Ե� ���� ����
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
        // ���� ������ - �������� �ƴ� ��� �ı�
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
        // ���� �������� ����
        var prevSlot = _pointerOverSlot;

        // ���� �������� ����
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
    /// <summary> ������ ���� ���� �����ְų� ���߱� </summary>
    private void ShowOrHideItemTooltip()
    {
        // ���콺�� ��ȿ�� ������ ������ ���� �ö�� �ִٸ� ���� �����ֱ�
        bool isValid =
            _pointerOverSlot != null && _pointerOverSlot.HasItem && _pointerOverSlot.IsAccessible
            && (_pointerOverSlot != _beginDragSlot); // �巡�� ������ �����̸� �������� �ʱ�

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

            // �������� ���� �ִ� ���Ը� �ش�
            if (_beginDragSlot != null && _beginDragSlot.HasItem && _beginDragSlot.IsAccessible)
            {
                EditorLog($"Drag Begin : Slot [{_beginDragSlot.Index}]");

                // ��ġ ���, ���� ���
                _beginDragIconTransform = _beginDragSlot.IconRect.transform;
                _beginDragIconPoint = _beginDragIconTransform.position;
                _beginDragCursorPoint = Input.mousePosition;

                // �� ���� ���̱�
                _beginDragSlotSiblingIndex = _beginDragSlot.transform.GetSiblingIndex();
                _beginDragSlot.transform.SetAsLastSibling();

                // �ش� ������ ���̶���Ʈ �̹����� �����ܺ��� �ڿ� ��ġ��Ű��
                _beginDragSlot.SetHighlightOnTop(false);
            }
            else
            {
                _beginDragSlot = null;
            }
        }

        if (Input.GetMouseButtonDown(_rightClick))
        {
            //�κ��丮�� ���ư���
        }
    }
    /// <summary> �巡���ϴ� ���� </summary>
    private void OnPointerDrag()
    {
        if (_beginDragSlot == null) return;

        if (Input.GetMouseButton(_leftClick))
        {
            // ��ġ �̵�
            _beginDragIconTransform.position =
                _beginDragIconPoint + (Input.mousePosition - _beginDragCursorPoint);
        }
    }
    /// <summary> Ŭ���� �� ��� </summary>
    void OnPointerUp()
    {
        if (Input.GetMouseButtonUp(_leftClick))
        {
            // End Drag
            if (_beginDragSlot != null)
            {
                // ��ġ ����
                _beginDragIconTransform.position = _beginDragIconPoint;

                // UI ���� ����
                _beginDragSlot.transform.SetSiblingIndex(_beginDragSlotSiblingIndex);

                // �巡�� �Ϸ� ó��
                EndDrag();

                // �ش� ������ ���̶���Ʈ �̹����� �����ܺ��� �տ� ��ġ��Ű��
                _beginDragSlot.SetHighlightOnTop(true);

                // ���� ����
                _beginDragSlot = null;
                _beginDragIconTransform = null;
            }
        }
    }
    private void EndDrag()
    {
        ItemSlotUI endDragSlot = RaycastAndGetFirstComponent<ItemSlotUI>();

        // ������ ���Գ��� ������ ��ȯ �Ǵ� �̵�
        if (endDragSlot != null && endDragSlot.IsAccessible)
        {            TrySwapItems(_beginDragSlot, endDragSlot);

            // ���� ����
            UpdateTooltipUI(endDragSlot);
            return;
        }

        // ������(Ŀ���� UI ����ĳ��Ʈ Ÿ�� ���� ���� ���� ���)
        if (!IsOverUI())
        {
            // Ȯ�� �˾� ���� �ݹ� ����
            int index = _beginDragSlot.Index;
            string itemName = Inventory.Instance.GetItemName(index);
            int amount = Inventory.Instance.GetCurrentAmount(index);

            // �� �� �ִ� �������� ���, ���� ǥ��
            if (amount > 1)
                itemName += $" x{amount}";

            if (_showRemovingPopup)
                _popup.OpenConfirmationPopup(() => TryRemoveItem(index), itemName);
            else
                TryRemoveItem(index);
        }
        // ������ �ƴ� �ٸ� UI ���� ���� ���
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
        // �� ������ ���
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
        //�κ��丮 ���� ������ ���ؿϷ� ���Ժ��� ���� �� �Ǵ�
        if (Inventory.Instance.GetEmptySlotCount() - (_afterSlotUIList.Count - ListEmptyRemain()) < 0)
        {
            Debug.Log("�κ��丮�� �� á���!");
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
    
    /// <summary> �������� �߰��Ҷ����� �׷��̵� ���� after����Ʈ�� ������� ������ </summary>
    public bool UpdateAfterDestroy(ItemSlotUI itemSlotUI)
    {
        if (gradeToMaterials.ContainsKey(itemSlotUI.item.itemGrade))
        {
            //���� ������ ������ ������ false��ȯ
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
