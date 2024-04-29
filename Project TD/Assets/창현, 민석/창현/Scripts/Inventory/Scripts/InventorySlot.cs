using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;


public enum SlotTag { None, Head, Chest, Legs, Feet, HotSlot, Weapon}

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public InventoryItem myItem { get; set; }

    public SlotTag myTag;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inventory.carriedItem == null) return;
            if (myTag != SlotTag.None && myTag != SlotTag.HotSlot && Inventory.carriedItem.myItem.itemTag != myTag) return;
            if (this.myItem != null) return;
            SetItem(Inventory.carriedItem);
        }
    }

    public void SetItem(InventoryItem item)
    {

        // Reset old slot
        item.activeSlot.myItem = null;

        // Set current slot
        myItem = item;
        myItem.activeSlot = this;
        myItem.transform.SetParent(transform);
        myItem.canvasGroup.blocksRaycasts = true;

        if (myTag == SlotTag.HotSlot)
        {
            InventoryItem copyItem = Instantiate(item, Inventory.Singleton.sceneHotBarSlots[transform.GetSiblingIndex()].transform);
            copyItem.Initialize(Inventory.carriedItem.myItem, Inventory.Singleton.sceneHotBarSlots[transform.GetSiblingIndex()]);
            copyItem.GetComponent<Image>().raycastTarget = false;
        }
        if(myTag == SlotTag.Weapon)
        {
            InventoryItem copyItem = Instantiate(item, Inventory.Singleton.weaponSlots[transform.GetSiblingIndex()].transform);
            copyItem.Initialize(Inventory.carriedItem.myItem, Inventory.Singleton.weaponSlots[transform.GetSiblingIndex()]);
            copyItem.GetComponent<Image>().raycastTarget = false;

            Inventory.Singleton.EquipEquipment(SlotTag.Weapon, item);
        }
        Inventory.carriedItem = null;
    }

    
}
