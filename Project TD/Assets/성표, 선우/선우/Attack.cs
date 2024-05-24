using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour, IAttack
{
    [SerializeField]
    PlayerStat _playerStat;
    [SerializeField]
    MonsterStat _monsterStat;

    public void OnAttacked(Stat attacker)
    {
        int damage = 0;
        _playerStat = attacker as PlayerStat; // attacker가 PlayerStat을 가지는 경우
        if (_playerStat != null)
        {
            damage = Mathf.Max(0, _playerStat.Attack - _monsterStat.Defense);
            _monsterStat.Hp -= damage;
            if (_playerStat.Hp <= 0)
            {
                _playerStat.Hp = 0;
                Managers.Game.Despawn(gameObject); // PlayerDespawn은 없을 예정이니 고쳐야함
            }
            Debug.Log("Player Attack");
        }

        _monsterStat = attacker as MonsterStat; // attacker가 MonsterStat을 가지는 경우
        if (_monsterStat != null)
        {
            damage = Mathf.Max(0, _monsterStat.Attack - _monsterStat.Defense);
            _monsterStat.Hp -= damage;
            if (_monsterStat.Hp <= 0)
            {
                _monsterStat.Hp = 0;
                Managers.Game.Despawn(gameObject);
                Debug.Log("Monster Attack");
            }
        }
    }
}
