using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTowerController : TowerController
{
    public ParticleSystem _fireParticle;
    public float _oilConsumptionPerShot = 1f;
    private float _currentOil = 100f;

    protected override void Start()
    {
        base.Start();
        _currentOil = 100f;
    }

    protected override void Update()
    {
        if (_isDisabled || _currentOil <= 0)
        {
            if (_fireParticle.isPlaying)
                _fireParticle.Stop();
            return;
        }

        _target = UpdateTarget();

        if (_target != null)
        {
            RotateTowardsTarget();
            _fireParticle.transform.position = _firePoint.position; // 파티클 위치 조정
            _fireParticle.transform.rotation = _firePoint.rotation; // 파티클 방향 조정
            // firePoint에서 타겟까지의 거리를 계산
            float distanceToTarget = Vector3.Distance(_firePoint.position, _target.transform.position);
            AdjustParticleSettings(distanceToTarget);
            if (!_fireParticle.isPlaying)
                _fireParticle.Play();
            ConsumeOil();
        }
        else
        {
            if (_fireParticle.isPlaying)
                _fireParticle.Stop();
        }
    }
    

    void ConsumeOil()
    {
        _currentOil -= _oilConsumptionPerShot * Time.deltaTime;
        if (_currentOil < 0)
            _currentOil = 0;
    }

    void AdjustParticleSettings(float distance)
    {
        var main = _fireParticle.main;
        // 타깃까지의 거리에 따라 속도를 조절합니다.
        float adjustedSpeed = Mathf.Lerp(50f, 120f, distance / _towerStat.AttackRange);
        main.startSpeed = adjustedSpeed;  // 속도 조절

        // 수명은 타깃까지의 이동 시간에 따라 조절합니다.
        float lifeTime = distance / adjustedSpeed + 0.5f;
        main.startLifetime = lifeTime; // 수명 조절
    }

    public void RefillOil(float amount)
    {
        _currentOil += amount;
        if (_currentOil > 100f)
            _currentOil = 100f;
    }
}
