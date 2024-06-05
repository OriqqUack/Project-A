using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuicideBomberController : MonsterController
{
    public enum DespawnReason { SelfDestruct, Attacked } // ���� ���� (���� or ���Ӵ���)
    public event Action<DespawnReason> OnDespawn; // �������� �Ҹ�� �� �̺�Ʈ

    private float explosionRadius = 4.0f; // ���� ����
    private int explosionDamage = 50; // ���� ������

    

    [SerializeField]
    private LayerMask targetLayerMask; // �������� �������� �� Ÿ�극�̾��ũ

    public override void Init()
    {
        base.Init();
    }

    protected override void UpdateMoving()
    {
        if (_lockTarget == null)
        {
            _lockTarget = FindClosestObject();
        }

        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            _navMeshAgent.SetDestination(_destPos);
            _navMeshAgent.speed = _stat.MoveSpeed;

            Vector3 dir = _destPos - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

            // ���� ���� ���� Ÿ���� ������ ����
            float distance = Vector3.Distance(transform.position, _lockTarget.transform.position);
            if (distance <= _stat.AttackRange)
            {
                Despawn(DespawnReason.SelfDestruct);
            }
        }
    }

    // �� �̺�Ʈ�� ȣ���ϴ� ������ �����ؾ���
    public void OnAttacked()
    {
        Despawn(DespawnReason.Attacked);
    }

    // �������� �����ɶ� ���� ������ ������ ������ Stat�� �ִ� �������� ���� ����
    private void Despawn(DespawnReason reason)
    {
        OnDespawn?.Invoke(reason);
        if (reason == DespawnReason.SelfDestruct)
        {
            Explode();
        }
        Managers.Game.Despawn(gameObject);
    }

    private void Explode()
    {
        // ���� ����: ���� ���� ���� �ִ� ������ �������� ����
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, targetLayerMask);
        foreach (var hitCollider in hitColliders)
        {
            Stat targetStat = hitCollider.GetComponent<Stat>();
            if (targetStat != null)
            {
                targetStat.OnAttacked(_stat);
            }
        }
    }
}
