using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();
    public Dictionary<int, Data.JsonItem> ShopItemData { get; private set; } = new Dictionary<int, Data.JsonItem>();
    public Dictionary<int, Data.JsonWeapon> ShopWeaponData { get; private set; } = new Dictionary<int, Data.JsonWeapon>();
    public Dictionary<int, Data.JsonShopWeaponGradePercentage> ShopWeaponPer { get; private set; } = new Dictionary<int, Data.JsonShopWeaponGradePercentage>();


    public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
        ShopItemData = LoadJson<Data.ShopItemData, int, Data.JsonItem>("item_weapon").MakeDict();
        ShopWeaponData = LoadJson<Data.ShopWeaponData, int, Data.JsonWeapon>("item_weapon").MakeDict();
        ShopWeaponPer = LoadJson<Data.WeaponPerData, int, Data.JsonShopWeaponGradePercentage>("shop_weapon_grade_percentage").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        try
        {
            return JsonUtility.FromJson<Loader>(textAsset.text);
        }
        catch (Exception e)
        {
            Debug.LogError($"JSON 파싱 오류: {e}");
            return default(Loader);
        }
    }
}
