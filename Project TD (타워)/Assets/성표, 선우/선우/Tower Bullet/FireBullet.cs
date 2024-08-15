using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public TowerStat towerStat;
    public float radius = 5f; // 화염이 닿는 범위
    public LayerMask targetLayer; // 데미지를 입힐 대상의 레이어

    private float damageInterval = 0.5f; // 데미지 적용 간격
    private float nextDamageTime = 0f; // 다음 데미지 적용 시간

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
                targetStat.OnAttacked(towerStat); // 화염이 닿은 타겟에 데미지 적용
            }
        }
    }
}
