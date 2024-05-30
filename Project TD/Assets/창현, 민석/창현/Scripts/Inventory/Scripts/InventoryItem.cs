using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Image itemIcon;
    public CanvasGroup canvasGroup { get; private set; }

    public Item myItem { get; set; }
    public InventorySlot activeSlot { get; set; }

    public Vector3 startPosition;
    public Transform startParent;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();
    }

    public void Initialize(Item item, InventorySlot parent)
    {
        activeSlot = parent;
        activeSlot.myItem = this;
        myItem = item;
        itemIcon.sprite = item.sprite;
    }

    //아이템 클릭하면 draggable로 이동
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        startParent = transform.parent;

        Inventory1.Singleton.OnClickedItem.Invoke(myItem);

        Inventory1.Singleton.SetCarriedItem(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        canvasGroup.blocksRaycasts = true;
        if(transform.parent == Inventory1.Singleton.draggablesTransform)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
    }
}
