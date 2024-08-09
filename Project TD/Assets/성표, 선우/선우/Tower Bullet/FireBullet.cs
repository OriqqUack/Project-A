using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public TowerStat _towerStat;
    public float _radius = 5f; // ȭ���� ��� ����
    public LayerMask _targetLayer; // �������� ���� ����� ���̾�

    private float _damageInterval = 0.5f; // ������ ���� ����
    private float _nextDamageTime = 0f; // ���� ������ ���� �ð�

    void Start()
    {
        _towerStat = GetComponentInParent<TowerStat>();
        _targetLayer = LayerMask.GetMask("Monster");
    }

    void Update()
    {
        if (Time.time >= _nextDamageTime)
        {
            ApplyDamageInRadius();
            _nextDamageTime = Time.time + _damageInterval;
        }
    }

    void ApplyDamageInRadius()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius, _targetLayer);
        foreach (Collider hitCollider in hitColliders)
        {
            Stat targetStat = hitCollider.GetComponent<Stat>();
            if (targetStat != null)
            {
                targetStat.OnAttacked(_towerStat); // ȭ���� ���� Ÿ�ٿ� ������ ����
            }
        }
    }
}
