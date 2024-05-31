using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rito.InventorySystem;

public class UI_Test : MonoBehaviour
{
    public Inventory _inventory;

    public ItemData[] _itemDataArray;

    [Space(12)]
    public Button _removeAllButton;

    [Space(1)]
    public Button _AddItem;

    private void Start()
    {
        if (_itemDataArray?.Length > 0)
        {
            for (int i = 0; i < _itemDataArray.Length; i++)
            {
                _inventory.Add(_itemDataArray[i], 3);

                if (_itemDataArray[i] is CountableItemData)
                    _inventory.Add(_itemDataArray[i], 255);
            }
        }

        _removeAllButton.onClick.AddListener(() =>
        {
            int capacity = _inventory.Capacity;
            for (int i = 0; i < capacity; i++)
                _inventory.Remove(i);
        });

        _AddItem.onClick.AddListener(() => _inventory.Add(_itemDataArray[1]));
    }
}
