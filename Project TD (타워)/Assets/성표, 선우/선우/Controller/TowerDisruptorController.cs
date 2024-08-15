using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDisruptorController : SoulController
{
    private float disableChance = 0.7f; // ���� Ȯ��

    public override void Init()
    {
        base.Init();
        // �ʱ�ȭ ���� �߰�
    }

    protected override void UpdateMoving()
    {
        // ���� ������ �� �̵����� ����
        if (_stat._isStunning)
        {
            State = Define.State.Stun;
            return;
        }

        if (_lockTarget != null)
        {
            GameObject closestObject = FindClosestObject();

            if (closestObject != rocket && closestObject != _lockTarget)
            {
                _lockTarget = closestObject;
            }

            _destPos = _lockTarget.transform.position;
            _navMeshAgent.SetDestination(_destPos);
            _navMeshAgent.speed = _stat.MoveSpeed;

            Vector3 dir = _destPos - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

            float distanceAttack = (_destPos - transform.position).magnitude;
            if (distanceAttack <= _stat.AttackRange)
            {
                _navMeshAgent.SetDestination(transform.position);
                State = Define.State.Skill;
                return;
            }
        }
    }

    // Ÿ�� ���� ����� �߰��ؾ���.
    protected override void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            // ü��
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            TowerController tower = _lockTarget.GetComponent<TowerController>();

            targetStat.OnAttacked(_stat);

            if (tower != null && !tower.isDisabled)
            {
                TryDisableTower(tower);
            }

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

    private void TryDisableTower(TowerController tower)
    {
        if (Random.value < disableChance)
        {
            tower.DisableTower();
        }
    }

}
