using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterStat : Stat
{
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

    // ���� �׷��� Define.Monsters���� � ��������
    // Ÿ���� �޾ƿ´ٰ� ġ�� �� Ÿ���� �־ ������ ������ �ڵ�����
    // ������ �� �ְԲ� �������ϴµ� ��� �ؾ��ұ�
    // �±׷� �ұ�? �ƴ� Ÿ������ �ٷ� �����ؼ� �ִ°� �����ұ�?

    protected int _stunCount = 0; // ���� Ƚ���� ����
    protected int _maxStunCount = 2; // ���� �ִ� Ƚ�� 
    public bool _isStunning = false; // ���������� ����
    protected float _stunDuration = 3.0f; // ���� �ð�
    protected Coroutine _stunCoroutine; // ���� �ڷ�ƾ ����
    protected float nextStunHpThreshold; // ���� ���� ü�°� ����
    
    protected virtual void Start()
    {
        SetStat(MonsterName);
        nextStunHpThreshold = MaxHp * 2 / 3; // ù��° ���� ���� ü�� ����
    }

    // ������ üũ�ϰ� ����
    public override void OnAttacked(Stat attacker)
    {
        float baseDamage = Mathf.Max(0, attacker.Attack - Defense);
        float damage = baseDamage;

        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);
        }
        else
        {
            if (Hp <= nextStunHpThreshold && _stunCount < _maxStunCount && !_isStunning)
            {
                if (_stunCoroutine != null)
                    StopCoroutine(_stunCoroutine);
                _stunCoroutine = StartCoroutine(Co_Stun());
            }
        }
    }

    // ���� ���� �ڷ�ƾ(�ѹ��� �ɸ��� true�� �ٲ� �Ȱɸ�)
    // �������϶� false���ٰ� �ڷ�ƾ�� ������ true�� �ٲ�� ������ �ʿ� = _isStunning.
    protected virtual IEnumerator Co_Stun()
    {
        _stunCount++;
        _isStunning = true;
        yield return new WaitForSeconds(_stunDuration);
        _isStunning = false;
        UpdateNextStunHpThreshold();
    }

    protected virtual void UpdateNextStunHpThreshold()
    {
        nextStunHpThreshold = MaxHp * (_maxStunCount - _stunCount) / 3;
    }

    public void SetStat(string monsterName)
    {
        Dictionary<string , Data.MonsterStat> dict = Managers.Data.MonsterDict;
        Data.MonsterStat monsterStat = dict[monsterName];
        _monsterName = monsterStat.monsterName;
        _hp = monsterStat.maxHp;
        _maxHp = monsterStat.maxHp;
        _attack = monsterStat.attack;
        _moveSpeed = monsterStat.moveSpeed;
        _attackSpeed = monsterStat.attackSpeed;
        _scanRange = monsterStat.scanRange;
        _attackRange = monsterStat.attackRange;
    }
}
