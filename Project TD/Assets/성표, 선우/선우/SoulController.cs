using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulController : MonsterController
{
    protected override void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            // 체력
            Stat targetStat = _lockTarget.GetComponent<Stat>();

            // 추가 피해를 계산하여 적용
            int originalAttack = _stat.Attack;
            int additionalDamage = Mathf.CeilToInt(originalAttack * 0.1f); // 10% 추가 피해
            _stat.Attack += additionalDamage;

            targetStat.OnAttacked(_stat);

            // 원래 공격력으로 복원
            _stat.Attack = originalAttack;

            if (targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _stat.AttackRange)
                    State = Define.State.Skill;
                else
                    State = Define.State.Moving;
            }
            else
            {
                State = Define.State.Idle;
            }
        }
        else
        {
            State = Define.State.Idle;
        }
    }
}
