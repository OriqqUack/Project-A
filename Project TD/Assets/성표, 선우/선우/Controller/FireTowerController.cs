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
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
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

    void RotateTowardsTarget()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - firePoint.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            firePoint.rotation = Quaternion.Lerp(firePoint.rotation, lookRotation, Time.deltaTime * 5f);
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
        main.startSpeed = Mathf.Lerp(5f, 20f, distance / _towerStat.AttackRange);
        main.startLifetime = Mathf.Lerp(1f, 5f, distance / _towerStat.AttackRange);
    }

    public void RefillOil(float amount)
    {
        currentOil += amount;
        if (currentOil > 100f)
            currentOil = 100f;
    }
}
