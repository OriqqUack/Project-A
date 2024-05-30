using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected int _defense;
    [SerializeField]
    protected float _moveSpeed;
    [SerializeField]
    protected float _dashingPower;
    [SerializeField]
    protected float _dashingTime;
    [SerializeField]
    protected float _dashingCooldown;
    [SerializeField]
    protected float _attackDistance;

    public Action<int> OnLevelChanged;
    public Action<int> OnHpChanged;
    public Action<int> OnMaxHpChanged;
    public Action<float> OnMoveSpeedChanged;
    public Action<int> OnAttackChanged;

    public int Level 
    {
        get { return _level; } 
        set 
        { 
            _level = value;
            OnLevelChanged?.Invoke(_level);
        } 
    }
    public int Hp 
    { 
        get { return _hp; } 
        set 
        { 
            _hp = value;
            OnHpChanged?.Invoke(_hp);
        } 
    }
    public int MaxHp 
    {   
        get { return _maxHp; } 
        set
        { 
            _maxHp = value;
            OnMaxHpChanged?.Invoke(_maxHp);
        } 
    }
    public int Attack 
    { 
        get { return _attack; } 
        set 
        { 
            _attack = value;
            OnAttackChanged?.Invoke(_attack);
        } 
    }
    public int Defense { get { return _defense; } set { _defense = value; } }
    public float MoveSpeed 
    { 
        get { return _moveSpeed; } 
        set 
        { 
            _moveSpeed = value;
            OnMoveSpeedChanged?.Invoke(_moveSpeed);
        } 
    }
    public float DashingSpeed { get { return _dashingPower; } set { _dashingPower = value; } }
    public float DashingTime { get { return _dashingTime; } set { _dashingTime = value; } }
    public float DashingCooldown { get { return _dashingCooldown; } set { _dashingCooldown = value; } }
    public float AttackDistance { get { return _attackDistance; } set { _attackDistance = value; } }

    private void Start()
    {
        _moveSpeed = 5.0f;
        /*_level = 1;
        _hp = 100;
        _maxHp = 100;
        _attack = 10;
        _defense = 5;
        _moveSpeed = 5.0f;
        _dashingPower = 10.0f;
        _dashingTime = 0.2f;
        _dashingCooldown = 0.1f;*/
    }

    public virtual void OnAttacked(Stat attacker)
    {
        int damage = Mathf.Max(0, attacker.Attack - Defense);
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);
        }
    }

    protected virtual void OnDead(Stat attacker)
    {
        PlayerStat playerStat = attacker as PlayerStat;
        if (playerStat != null)
        {
            playerStat.Exp += 15;
        }

        //Managers.Game.Despawn(gameObject);
    }
}
