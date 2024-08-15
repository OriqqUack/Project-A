using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulStat : MonsterStat
{
    public override void OnAttacked(Stat attacker)
    {
        if (attacker == null)
        {
            Debug.LogError("Attacker is null in SoulStat.OnAttacked");
            return;
        }

        float baseDamage = Mathf.Max(0, attacker.Attack - Defense);
        float damage = baseDamage;

        //BaseController attackerController = attacker.GetComponent<BaseController>();
        //if (attackerController != null && attackerController.WorldObjectType == Define.WorldObject.Player) // 플레이어에게 공격 받을 시
        //{
        //    int additionalDamage = Mathf.FloorToInt(baseDamage * 0.1f); // 10프로 데미지 추가
        //    damage += additionalDamage;
        //}

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
}
