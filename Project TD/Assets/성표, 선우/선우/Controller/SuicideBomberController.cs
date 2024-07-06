using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;

public class SuicideBomberController : MonsterController
{
    public enum DespawnReason { SelfDestruct, Attacked } // ���� ���� (���� or ���Ӵ���)
    public event Action<DespawnReason> OnDespawn; // �������� �Ҹ�� �� �̺�Ʈ

    private float explosionRadius = 4.0f; // ���� ����
    private float detonationTime = 3.0f; // ��ȭ �� ���߱����� �ð�
    private Coroutine detonationCoroutine; // ��ȭ �ڷ�ƾ


    public override void Init()
    {
        base.Init();
    }

    protected override void UpdateMoving()
    {
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

            // ���� ���� ���� Ÿ���� ������ ����
            float distance = Vector3.Distance(transform.position, _lockTarget.transform.position);
            if (distance <= _stat.AttackRange && detonationCoroutine == null)
            {
                detonationCoroutine = StartCoroutine(DetonationCoroutine());
            }
        }
    }

    // �� �޼���� �������� ������ �޾��� �� ȣ��
    public void OnAttacked()
    {
        Despawn(DespawnReason.Attacked);
    }

    // �������� �����ɶ� ���� ������ ������ ������ Stat�� �ִ� �������� ���� ����
    private void Despawn(DespawnReason reason)
    {
        // ��ȭ�� �ڿ� �׾��ٸ� �ڷ�ƾ ����
        if (detonationCoroutine != null)
        {
            StopCoroutine(detonationCoroutine);
            detonationCoroutine = null;
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
    
    // ��ȭ �ڷ�ƾ
    private IEnumerator DetonationCoroutine()
    {
        yield return new WaitForSeconds(detonationTime);
        if (detonationCoroutine != null) // �ڷ�ƾ�� ���������� �Ϸ�� ��쿡�� ����
        {
            Despawn(DespawnReason.SelfDestruct);
        }
    }
}
