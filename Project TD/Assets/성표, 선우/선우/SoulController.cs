using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulController : MonsterController
{
    protected override void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            // ü��
            Stat targetStat = _lockTarget.GetComponent<Stat>();

            // �߰� ���ظ� ����Ͽ� ����
            int originalAttack = _stat.Attack;
            int additionalDamage = Mathf.CeilToInt(originalAttack * 0.1f); // 10% �߰� ����
            _stat.Attack += additionalDamage;

            targetStat.OnAttacked(_stat);

            // ���� ���ݷ����� ����
            _stat.Attack = originalAttack;

            if (targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _stat.AttackRange)
                    State = Define.State.Skill;
                else
                    State = Define.State.Moving;

                // ���� ���� üũ �� ����
                if (_stat.Hp <= _stat.MaxHp / 3 && stunCount < maxStunCount && !_isStunning)
                {
                    if (_stunCoroutine != null)
                        StopCoroutine(_stunCoroutine);
                    _stunCoroutine = StartCoroutine(Stun());
                }
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
