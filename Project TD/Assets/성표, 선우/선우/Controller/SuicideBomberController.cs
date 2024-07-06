using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuicideBomberController : MonsterController
{
    public enum DespawnReason { SelfDestruct, Attacked } // 죽은 이유 (자폭 or 죽임당함)
    public event Action<DespawnReason> OnDespawn; // 자폭병이 소멸될 때 이벤트

    private float explosionRadius = 4.0f; // 자폭 범위
    private int explosionDamage = 50; // 자폭 데미지

    

    [SerializeField]
    private LayerMask targetLayerMask; // 자폭병이 데미지를 줄 타깃레이어마스크

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

            // 공격 범위 내에 타겟이 들어오면 자폭
            float distance = Vector3.Distance(transform.position, _lockTarget.transform.position);
            if (distance <= _stat.AttackRange)
            {
                Despawn(DespawnReason.SelfDestruct);
            }
        }
    }

    // 이 이벤트를 호출하는 로직이 존재해야함
    public void OnAttacked()
    {
        Despawn(DespawnReason.Attacked);
    }

    // 자폭병이 디스폰될때 따로 구현될 로직이 없으니 Stat에 있는 디스폰과는 따로 구현
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
        // 자폭 로직: 일정 범위 내에 있는 적에게 데미지를 입힘
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
