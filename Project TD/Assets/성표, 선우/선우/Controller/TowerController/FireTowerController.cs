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
            _fireParticle.transform.position = _firePoint.position; // ��ƼŬ ��ġ ����
            _fireParticle.transform.rotation = _firePoint.rotation; // ��ƼŬ ���� ����
            // firePoint���� Ÿ�ٱ����� �Ÿ��� ���
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
        // Ÿ������� �Ÿ��� ���� �ӵ��� �����մϴ�.
        float adjustedSpeed = Mathf.Lerp(50f, 120f, distance / _towerStat.AttackRange);
        main.startSpeed = adjustedSpeed;  // �ӵ� ����

        // ������ Ÿ������� �̵� �ð��� ���� �����մϴ�.
        float lifeTime = distance / adjustedSpeed + 0.5f;
        main.startLifetime = lifeTime; // ���� ����
    }

    public void RefillOil(float amount)
    {
        _currentOil += amount;
        if (_currentOil > 100f)
            _currentOil = 100f;
    }
}
