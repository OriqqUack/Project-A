using Rito.InventorySystem;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemData> requiredItems;
    public ItemData result;
    public bool isDiscovered = false;
}
