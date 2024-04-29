using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonItem : RawData
{
    public string item_name;
    public string item_description;
    public int kind_of_money;
    public int price;
    public int item_type;
}

public class JsonWeapon : JsonItem
{
    public float attack_power;
    public float critical_attack_power;
    public float critical_chance;
    public float speed_of_attack;
    public int item_grade;
    public int weapon_type;
}


public class JsonShopWeaponGradePercentage : RawData
{
    public int grade;
    public string grade_name;
    public float percentage;
}
