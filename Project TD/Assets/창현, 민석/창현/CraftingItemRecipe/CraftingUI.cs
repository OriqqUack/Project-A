using Rito.InventorySystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    public GameObject recipeButtonPrefab;
    public Transform contentPanel; // Scroll View's Content panel

    public List<GameObject> updatedRecipe = new List<GameObject>();

    private void Awake()
    {
    }

    private void Start()
    {
        UpdateCraftingUI();
    }

    public void UpdateCraftingUI()
    {
        foreach(var recipeObject in updatedRecipe)
        {
            Managers.Resource.Destroy(recipeObject);
        }

        foreach(var recipe in Inventory.Instance.craftingSystem.recipes)
        {
            if (recipe.isDiscovered)
            {
                GameObject craftingItemSlot = Managers.Resource.Instantiate("UI/CraftingItemSlot", contentPanel);
                Button buttonComponent = craftingItemSlot.transform.GetChild(0).GetComponent<Button>();

                buttonComponent.interactable = Inventory.Instance.craftingSystem.CanCraft(recipe);
                ////////////////////스프라이트 생성//////////////////////
                craftingItemSlot.transform.GetChild(0).GetComponent<ItemSlotUI>().SetItem(recipe.result.IconSprite);
                craftingItemSlot.transform.GetChild(0).GetComponent<ItemSlotUI>().item = recipe.result;
                craftingItemSlot.transform.GetChild(0).GetComponent<ItemSlotUI>().SetSlotIndex(40);
                Transform resSection = craftingItemSlot.transform.Find("NeedResSection");

                updatedRecipe.Add(craftingItemSlot);

                if (craftingItemSlot.GetComponent<Made>() == null)
                {
                    buttonComponent.onClick.AddListener(() => Inventory.Instance.craftingSystem.CraftItem(recipe));

                    foreach (var needItem in recipe.requiredItems)
                    {
                        GameObject item = Managers.Resource.Instantiate("UI/ItemSlot", resSection);
                        item.GetComponent<ItemSlotUI>().item = needItem;
                        item.GetComponent<ItemSlotUI>().SetSlotIndex(40);
                        item.GetComponent<ItemSlotUI>().SetItem(needItem.IconSprite);
                    }
                    craftingItemSlot.AddComponent<Made>();
                }
            }
        }
        
    }
}