using Rito.InventorySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemAmount
{
    public ItemData item;
    [Range(1, 999)]
    public int Amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;

    public bool CanCraft(Inventory inventory)
    {
        foreach(ItemAmount itemAmount in Materials)
        {
        }
        return false;
    }

    public void Craft(Inventory inventory)
    {

    }
}
