using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public TowerStat _towerStat;
    public float _radius = 5f; // 화염이 닿는 범위
    public LayerMask _targetLayer; // 데미지를 입힐 대상의 레이어

    private float _damageInterval = 0.5f; // 데미지 적용 간격
    private float _nextDamageTime = 0f; // 다음 데미지 적용 시간

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
                targetStat.OnAttacked(_towerStat); // 화염이 닿은 타겟에 데미지 적용
            }
        }
    }
}
