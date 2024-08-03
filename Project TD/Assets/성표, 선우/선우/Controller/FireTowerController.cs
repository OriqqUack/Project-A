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
            fireParticle.transform.position = firePoint.position; // ��ƼŬ ��ġ ����
            fireParticle.transform.rotation = firePoint.rotation; // ��ƼŬ ���� ����
            // firePoint���� Ÿ�ٱ����� �Ÿ��� ���
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
        // Ÿ������� �Ÿ��� ���� �ӵ��� �����մϴ�.
        float adjustedSpeed = Mathf.Lerp(50f, 120f, distance / _towerStat.AttackRange);
        main.startSpeed = adjustedSpeed;  // �ӵ� ����

        // ������ Ÿ������� �̵� �ð��� ���� �����մϴ�.
        float lifeTime = distance / adjustedSpeed + 0.5f;
        main.startLifetime = lifeTime; // ���� ����
    }

    public void RefillOil(float amount)
    {
        currentOil += amount;
        if (currentOil > 100f)
            currentOil = 100f;
    }
}
