using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string ItemName;
    public Sprite sprite;
    public SlotTag itemTag;

    [Header("If the item can be equipped")]
    public GameObject equipmentPrefab;
}
