using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTowerController : TowerController
{
    public ParticleSystem fireParticle;
    public float oilConsumptionPerShot = 1f;
    private float currentOil = 100f;

    protected override void Start()
    {
        base.Start();
        currentOil = 100f;
    }

    protected override void Update()
    {
        if (isDisabled || currentOil <= 0)
        {
            if (fireParticle.isPlaying)
                fireParticle.Stop();
            return;
        }

        target = UpdateTarget();

        if (target != null)
        {
            RotateTowardsTarget();
            fireParticle.transform.position = firePoint.position; // 파티클 위치 조정
            fireParticle.transform.rotation = firePoint.rotation; // 파티클 방향 조정
            // firePoint에서 타겟까지의 거리를 계산
            float distanceToTarget = Vector3.Distance(firePoint.position, target.transform.position);
            AdjustParticleSettings(distanceToTarget);
            if (!fireParticle.isPlaying)
                fireParticle.Play();
            ConsumeOil();
        }
        else
        {
            if (fireParticle.isPlaying)
                fireParticle.Stop();
        }
    }
    

    void ConsumeOil()
    {
        currentOil -= oilConsumptionPerShot * Time.deltaTime;
        if (currentOil < 0)
            currentOil = 0;
    }

    void AdjustParticleSettings(float distance)
    {
        var main = fireParticle.main;
        // 타깃까지의 거리에 따라 속도를 조절합니다.
        float adjustedSpeed = Mathf.Lerp(50f, 120f, distance / _towerStat.AttackRange);
        main.startSpeed = adjustedSpeed;  // 속도 조절

        // 수명은 타깃까지의 이동 시간에 따라 조절합니다.
        float lifeTime = distance / adjustedSpeed + 0.5f;
        main.startLifetime = lifeTime; // 수명 조절
    }

    public void RefillOil(float amount)
    {
        currentOil += amount;
        if (currentOil > 100f)
            currentOil = 100f;
    }
}
