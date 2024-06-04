using Rito.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RecipeTable : UI_Base
{
    [SerializeField] List<ItemSlotUI> slotUIs;

    enum GameObjects 
    {
        ItemSlot_1,
        ItemSlot_2,
        ItemSlot_3,
        ItemSlot_4,
        ResultItem
    }

    public override void Init()
    {
    }

}
