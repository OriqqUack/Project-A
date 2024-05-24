using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{ 
#region Stat
	[Serializable]
	public class Stat
	{
		public int level;
		public int maxHp;
		public int attack;
		public int totalExp;
	}

	[Serializable]
	public class StatData : ILoader<int, Stat>
	{
		public List<Stat> stats = new List<Stat>();

		public Dictionary<int, Stat> MakeDict()
		{
			Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
			foreach (Stat stat in stats)
				dict.Add(stat.level, stat);
			return dict;
		}
	}
    #endregion

    #region shop
    [Serializable]
    public class JsonItem
    {
        public int raw;
        public string item_name;
        public string item_description;
        public int kind_of_money;
        public int price;
        public int item_type;
    }

    [Serializable]
    public class JsonWeapon 
    {
        public int raw;
        public string item_name;
        public string item_description;
        public int kind_of_money;
        public int price;
        public int item_type;
        public float attack_power;
        public float critical_attack_power;
        public float critical_chance;
        public float speed_of_attack;
        public int item_grade;
        public int weapon_type;
    }

    [Serializable]
    public class JsonShopWeaponGradePercentage
    {
        public int raw;
        public int grade;
        public string grade_name;
        public float percentage;
    }


    [Serializable]
    public class ShopItemData : ILoader<int, JsonItem>
    {
        public List<JsonItem> itemWeapon = new List<JsonItem>();

        public Dictionary<int, JsonItem> MakeDict()
        {
            Dictionary<int, JsonItem> dict = new Dictionary<int, JsonItem>();
            foreach (JsonItem stat in itemWeapon)
                dict.Add(stat.raw, stat);
            return dict;
        }
    }

    [Serializable]
    public class ShopWeaponData : ILoader<int, JsonWeapon>
    {
        public List<JsonWeapon> itemWeapon = new List<JsonWeapon>();

        public Dictionary<int, JsonWeapon> MakeDict()
        {
            Dictionary<int, JsonWeapon> dict = new Dictionary<int, JsonWeapon>();
            foreach (JsonWeapon stat in itemWeapon)
                dict.Add(stat.raw, stat);
            return dict;
        }
    }

    [Serializable]
    public class WeaponPerData : ILoader<int, JsonShopWeaponGradePercentage>
    {
        public List<JsonShopWeaponGradePercentage> weaponPercentage = new List<JsonShopWeaponGradePercentage>();

        public Dictionary<int, JsonShopWeaponGradePercentage> MakeDict()
        {
            Dictionary<int, JsonShopWeaponGradePercentage> dict = new Dictionary<int, JsonShopWeaponGradePercentage>();
            foreach (JsonShopWeaponGradePercentage stat in weaponPercentage)
                dict.Add(stat.raw, stat);
            return dict;
        }
    }
    #endregion
}