using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerStat : Stat
{
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

    //public int Level { get { return _level; } set { _level = value; } }
    //public int Hp { get { return _hp; } set { _hp = value; } }
    //public int MaxHp { get { return _maxHp;} set { _maxHp = value; } } 
    //public int Attack { get { return _attack; } set { _attack = value; } } 
    //public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } } 
    public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
    public float ScanRange { get { return _scanRange; } set { _scanRange = value; } }
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }

    void Start()
    {

    }
}
