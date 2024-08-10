using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritStat : MonsterStat
{
    public override void OnAttacked(Stat attacker)
    {
        if (attacker == null)
        {
            Debug.LogError("Attacker is null in SoulStat.OnAttacked");
            return;
        }

        int baseDamage = Mathf.Max(0, attacker.Attack - Defense);
        int damage = baseDamage;

        // 공격자가 플레이어일 경우 10% 추가 데미지 적용
        BaseController attackerController = attacker.GetComponent<BaseController>();
        if (attackerController != null && attackerController.WorldObjectType == Define.WorldObject.Player)
        {
            int additionalDamage = Mathf.FloorToInt(baseDamage * 0.1f); // 10% 추가 데미지
            damage += additionalDamage;
        }

        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);
        }
        else
        {
            if (Hp <= _nextStunHpThreshold && _stunCount < _maxStunCount && !_isStunning)
            {
                if (_stunCoroutine != null)
                    StopCoroutine(_stunCoroutine);
                _stunCoroutine = StartCoroutine(Co_Stun());
            }
        }
    }
}
