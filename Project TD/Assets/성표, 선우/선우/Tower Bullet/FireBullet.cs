using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public TowerStat towerStat;
    public float radius = 5f; // ȭ���� ��� ����
    public LayerMask targetLayer; // �������� ���� ����� ���̾�

    private float damageInterval = 0.5f; // ������ ���� ����
    private float nextDamageTime = 0f; // ���� ������ ���� �ð�

    void Start()
    {
        towerStat = GetComponentInParent<TowerStat>();
    }

    void Update()
    {
        if (Time.time >= nextDamageTime)
        {
            ApplyDamageInRadius();
            nextDamageTime = Time.time + damageInterval;
        }
    }

    void ApplyDamageInRadius()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, targetLayer);
        foreach (Collider hitCollider in hitColliders)
        {
            Stat targetStat = hitCollider.GetComponent<Stat>();
            if (targetStat != null)
            {
                targetStat.OnAttacked(towerStat); // ȭ���� ���� Ÿ�ٿ� ������ ����
            }
        }
    }
}
