using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class Monster
{
    public int level;
    public int maxHp;
    public int attack;
    public float moveSpeed;
    public float attackSpeed;
    public float scanRange;
    public float attackRange;
}

[Serializable]
public class MonsterList
{
    public Dictionary<string, Monster> monsters;
}

public class MonsterJson : MonoBehaviour
{
    void Start()
    {
        Dictionary<string, Monster> monsterDic = new Dictionary<string, Monster>();

        //Monster slime = new Monster();
        //slime.level =;
        //slime.maxHp = 100;
        //slime.attack = 100;
        //slime.moveSpeed = 100;
        //slime.attackSpeed = 100;
        //slime.scanRange = 100;
        //slime.attackRange = 100;

        //Monster ork = new Monster();
        //ork.level = "Magic";
        //ork.maxHp = 30;
        //ork.attack = 30;
        //ork.moveSpeed = 30;
        //ork.attackSpeed = 30;
        //ork.scanRange = 30;
        //ork.attackRange = 30;

        //monsterDic["Sizard"] = slime;
        //monsterDic["Ork"] = ork;

        //MonsterList Monster = new MonsterList();
        //Monster.monsters = monsterDic;

        #region ToJson
        ////ToJson �κ�
        //string jsonData = DictionaryJsonUtility.ToJson(monsterDic, true);

        //string path = Application.dataPath + "/Data";
        //if (!Directory.Exists(path))
        //{
        //    Directory.CreateDirectory(path);
        //}
        //File.WriteAllText(path + "/MonsterDataEx.json", jsonData);
        #endregion

        //FromJson �κ�
        // Json ���Ͽ��� �����͸� �о��
        //string fromJsonData = File.ReadAllText(path + "/MonsterData.json");

        //// MonsterList Ŭ������ ��ü�� ����
        //MonsterList MonsterFromJson = new MonsterList();
        //// Json �����͸� ��ųʸ��� ������ȭ�Ͽ� MonsterList�� monsters �ʵ忡 ����
        //MonsterFromJson.monsters = DictionaryJsonUtility.FromJson<string, Monster>(fromJsonData);
        //// ������ȭ�� ��ųʸ��� ���
        //print(MonsterFromJson.monsters);
    }
}
