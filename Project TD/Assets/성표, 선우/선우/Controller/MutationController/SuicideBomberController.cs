using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;

public class SuicideBomberController : MonsterController
{
    public enum DespawnReason { SelfDestruct, Attacked } // 죽은 이유 (자폭 or 죽임당함)
    public event Action<DespawnReason> OnDespawn; // 자폭병이 소멸될 때 이벤트

    private float _explosionRadius = 4.0f; // 자폭 범위
    private float _detonationTime = 3.0f; // 점화 후 폭발까지의 시간
    private Coroutine _detonationCoroutine; // 점화 코루틴


    public override void Init()
    {
        base.Init();
    }

    protected override void UpdateMoving()
    {
        if (_lockTarget != null)
        {
            GameObject closestObject = FindClosestObject();

            if (closestObject != _rocket && closestObject != _lockTarget)
            {
                _lockTarget = closestObject;
               
            }
        
            _destPos = _lockTarget.transform.position;
            _navMeshAgent.SetDestination(_destPos);
            _navMeshAgent.speed = _stat.MoveSpeed;

            Vector3 dir = _destPos - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

            // 공격 범위 내에 타겟이 들어오면 자폭
            float distance = Vector3.Distance(transform.position, _lockTarget.transform.position);
            if (distance <= _stat.AttackRange && _detonationCoroutine == null)
            {
                _detonationCoroutine = StartCoroutine(Co_DetonationCoroutine());
            }
        }
    }

    // 이 메서드는 자폭병이 공격을 받았을 때 호출
    public void OnAttacked()
    {
        Despawn(DespawnReason.Attacked);
    }

    // 자폭병이 디스폰될때 따로 구현될 로직이 없으니 Stat에 있는 디스폰과는 따로 구현
    private void Despawn(DespawnReason reason)
    {
        // 점화된 뒤에 죽었다면 코루틴 중지
        if (_detonationCoroutine != null)
        {
            StopCoroutine(_detonationCoroutine);
            _detonationCoroutine = null;
        }

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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius, _targetLayerMask);
        foreach (var hitCollider in hitColliders)
        {
            Stat targetStat = hitCollider.GetComponent<Stat>();
            if (targetStat != null)
            {
                targetStat.OnAttacked(_stat);
            }
        }
    }
    
    // 점화 코루틴
    private IEnumerator Co_DetonationCoroutine()
    {
        yield return new WaitForSeconds(_detonationTime);
        if (_detonationCoroutine != null) // 코루틴이 정상적으로 완료된 경우에만 폭발
        {
            Despawn(DespawnReason.SelfDestruct);
        }
    }
}
