using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterStat : Stat
{
    private string objectTag;

    public void SetGameObjectTag(GameObject go)
    {
        objectTag = go.tag;
    }

    [SerializeField]
    protected string _monsterName;
    //[SerializeField]
    //protected int _level;
    //[SerializeField]
    //protected int _hp;
    //[SerializeField]
    //protected int _maxHp;
    //[SerializeField]
    //protected int _attack;
    //[SerializeField]
    //protected float _moveSpeed;
    [SerializeField]
    protected float _attackSpeed;
    [SerializeField]
    protected float _scanRange;
    [SerializeField]
    protected float _attackRange;

    public string MonsterName { get { return _monsterName; } set { _monsterName = value; } }
    //public int Level { get { return _level; } set { _level = value; } }
    //public int Hp { get { return _hp; } set { _hp = value; } }
    //public int MaxHp { get { return _maxHp;} set { _maxHp = value; } } 
    //public int Attack { get { return _attack; } set { _attack = value; } } 
    //public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } } 
    public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
    public float ScanRange { get { return _scanRange; } set { _scanRange = value; } }
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }

    // 지금 그러면 Define.Monsters에서 어떤 몬스터인지
    // 타입을 받아온다고 치면 그 타입을 넣어서 몬스터의 스텟을 자동으로
    // 설정할 수 있게끔 만들어야하는데 어떻게 해야할까
    // 태그로 할까? 아님 타입으로 바로 지정해서 넣는게 가능할까?

    void Start()
    {

        SetGameObjectTag(gameObject);
        //SetStat(objectTag);
    }


    //public void SetMonster(string monsterName)
    //{
    //    switch (MonsterType)
    //    {
    //        case Define.Monsters.Slime:
    //            SetStat("Slime");
    //            break;
    //        case Define.Monsters.Ork:
    //            SetStat("Ork");
    //            break;
    //    }
    //}

    public void SetStat(string monsterName)
    {
        /*Dictionary<string, Data.MonsterStat> dict = Managers.Data.MonsterDict;
        Data.MonsterStat monsterStat = dict[monsterName];
        _monsterName = monsterStat.monsterName;
        _hp = monsterStat.maxHp;
        _maxHp = monsterStat.maxHp;
        _attack = monsterStat.attack;
        _moveSpeed = monsterStat.moveSpeed;
        _attackSpeed = monsterStat.attackSpeed;
        _scanRange = monsterStat.scanRange;
        _attackRange = monsterStat.attackRange;*/
    }
}
