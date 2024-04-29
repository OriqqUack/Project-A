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
        _playerStat = attacker as PlayerStat; // attacker�� PlayerStat�� ������ ���
        if (_playerStat != null)
        {
            damage = Mathf.Max(0, _playerStat.Attack - _monsterStat.Defense);
            _monsterStat.Hp -= damage;
            if (_playerStat.Hp <= 0)
            {
                _playerStat.Hp = 0;
                Managers.Game.Despawn(gameObject); // PlayerDespawn�� ���� �����̴� ���ľ���
            }
            Debug.Log("Player Attack");
        }

        _monsterStat = attacker as MonsterStat; // attacker�� MonsterStat�� ������ ���
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
