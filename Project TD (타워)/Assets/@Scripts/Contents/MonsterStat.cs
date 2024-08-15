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

    // 지금 그러면 Define.Monsters에서 어떤 몬스터인지
    // 타입을 받아온다고 치면 그 타입을 넣어서 몬스터의 스텟을 자동으로
    // 설정할 수 있게끔 만들어야하는데 어떻게 해야할까
    // 태그로 할까? 아님 타입으로 바로 지정해서 넣는게 가능할까?

    protected int _stunCount = 0; // 경직 횟수를 축적
    protected int _maxStunCount = 2; // 경직 최대 횟수 
    public bool _isStunning = false; // 경직중인지 여부
    protected float _stunDuration = 3.0f; // 경직 시간
    protected Coroutine _stunCoroutine; // 경직 코루틴 변수
    protected float nextStunHpThreshold; // 경직 기준 체력값 설정
    
    protected virtual void Start()
    {
        SetStat(MonsterName);
        nextStunHpThreshold = MaxHp * 2 / 3; // 첫번째 경직 기준 체력 설정
    }

    // 경직을 체크하고 적용
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

    // 경직 상태 코루틴(한번만 걸리면 true로 바뀌어서 안걸림)
    // 스턴중일때 false였다가 코루틴이 끝나면 true로 바뀌는 변수가 필요 = _isStunning.
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
