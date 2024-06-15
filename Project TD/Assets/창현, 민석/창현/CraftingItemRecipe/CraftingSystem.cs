using Rito.InventorySystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public List<CraftingRecipe> recipes = new List<CraftingRecipe>();

    private void Awake()
    {
        recipes = Resources.LoadAll<CraftingRecipe>("CraftingRecipes/Recipes").ToList();

    }
    public void OnItemDiscovered(ItemData item)
    {
        foreach (var recipe in recipes)
        {
            if (recipe.requiredItems.Contains(item))
            {
                recipe.isDiscovered = true;
                Debug.Log("Recipe for " + recipe.result.Name + " discovered.");
                FindObjectOfType<CraftingUI>()?.UpdateCraftingUI();
            }
        }
    }

    public void CraftItem(CraftingRecipe recipe)
    {
        if (recipe.isDiscovered && CanCraft(recipe))
        {
            foreach (var item in recipe.requiredItems)
            {
                int index = Inventory.Instance.GetCurrentItemIndex(item);
                Inventory.Instance.RemoveCount(index);
            }
            Inventory.Instance.Add(recipe.result);
            Debug.Log("Crafted " + recipe.result.Name);
        }
        else
        {
            Debug.Log("Cannot craft " + recipe.result.Name);
        }
    }

    public bool CanCraft(CraftingRecipe recipe)
    {
        foreach (var item in recipe.requiredItems)
        {
            int index = Inventory.Instance.GetCurrentItemIndex(item);
            if (!Inventory.Instance.HasItem(index))
            {
                return false;
            }
        }
        return true;
    }
}