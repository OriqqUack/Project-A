using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashBullet : BombBullet
{
    public float radius = 5f;       // 스플래시 범위
    public LayerMask targetLayer;   // 데미지를 입힐 대상의 레이어

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void HitTarget()
    {
        // 여기에 적에게 데미지를 주는 로직을 구현
        Damage();
        Destroy(gameObject);
    }

    public override void Damage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, targetLayer);
        foreach (Collider hitCollider in hitColliders)
        {
            Stat targetStat = hitCollider.GetComponent<Stat>();
            if (targetStat != null)
            {
                targetStat.OnAttacked(_towerStat);
            }
        }
    }
}
