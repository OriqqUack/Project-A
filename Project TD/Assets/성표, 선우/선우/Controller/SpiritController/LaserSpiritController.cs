using System.Collections;
using UnityEngine;
using PixelArsenal;

public class LaserSpiritController : SpiritController
{
    [SerializeField] private float _damageIncreaseInterval = 3f; // ������ ���� ����
    [SerializeField] private float _damageIncreaseAmount = 1f; // �����ϴ� ���ط�
    [SerializeField] private PixelArsenalBeamStatic _laserBeam; // ������ �� ��ũ��Ʈ ����
    [SerializeField] private LaserDamage _laserDamage; // Co_DamageCoroutine�� �����ϱ� ���� ����

    private Coroutine _laserAttackCoroutine;
    private bool _isAttacking = false;
    private GameObject _previousTarget; // ���� Ÿ��
    private int _originalAttackValue; // ������ ���ݷ� ���� ����

    public override void Init()
    {
        base.Init();

        _laserDamage = GetComponentInChildren<LaserDamage>();

        if (_laserBeam == null)
        {
            Debug.LogError("������ �� ��ũ��Ʈ�� ������� �ʾҽ��ϴ�.");
            return;
        }

        _laserBeam.BeamLength = _stat.AttackRange; // ���� �ִ� ��Ÿ� ����
        _originalAttackValue = _stat.Attack; // ������ ���ݷ� ���� ����
        _laserBeam.Initialize(transform);  // LaserSpirit�� Transform�� ����
    }

    protected override void UpdateSkill()
    {
        if (_stat._isStunning)
        {
            State = Define.State.Stun;
            StopLaser(); // ������ ����
            return;
        }

        if (_lockTarget != null)
        {
            // Ÿ���� ����Ǿ����� Ȯ��
            if (_lockTarget != _previousTarget)
            {
                if (_laserAttackCoroutine != null)
                {
                    StopCoroutine(_laserAttackCoroutine);
                }

                StopLaser(); // ������ ����
                _isAttacking = false;
                _previousTarget = _lockTarget;
            }

            // �������� �ƴϰ� ���� Ȱ��ȭ�� ���°� �ƴ� ��쿡�� ���� ����
            if (!_isAttacking && !_laserBeam.IsBeamActive)
            {
                _isAttacking = true;
                _laserAttackCoroutine = StartCoroutine(Co_LaserAttack());
            }
        }

        RotateTowardsTarget(); // Ÿ���� ���� ȸ��
    }

    protected override void UpdateMoving()
    {
        if (_isAttacking)
        {
            StopLaser(); // ������ ����
            return;
        }

        if (_stat._isStunning)
        {
            State = Define.State.Stun;
            StopLaser(); // ������ ����
            return;
        }

        base.UpdateMoving();
    }

    private IEnumerator Co_LaserAttack()
    {
        float currentDamageIncrease = 0f;
        float timeSinceLastDamageIncrease = 0f;

        // ������ �� ��ũ��Ʈ ����
        _laserBeam.EnableBeam();

        // �������� ��ġ�� ���� ��ġ�� ����
        _laserBeam.transform.position = transform.position;
        _laserDamage.StartDamageCoroutine();

        while (_isAttacking && _lockTarget != null && !_stat._isStunning)
        {
            _laserBeam.transform.LookAt(_lockTarget.transform.position); // Ÿ���� ���� �������� �߻�

            // 3�ʸ��� ������ ������ ���� �ð� ���
            timeSinceLastDamageIncrease += Time.deltaTime;
            if (timeSinceLastDamageIncrease >= _damageIncreaseInterval)
            {
                currentDamageIncrease += _damageIncreaseAmount;
                _stat.Attack = _originalAttackValue + Mathf.FloorToInt(currentDamageIncrease);
                timeSinceLastDamageIncrease = 0f; // Ÿ�̸� �ʱ�ȭ
            }

            // ���� �����ӱ��� ���
            yield return null;
        }

        ResetAttack();
        StopLaser();
    }

    private void StopLaser()
    {
        _isAttacking = false;
        _laserBeam.DisableBeam();
        _laserDamage.StopDamageCoroutine();

        if (_laserAttackCoroutine != null)
        {
            StopCoroutine(_laserAttackCoroutine);
            _laserAttackCoroutine = null;
        }

        // ���� �� ���� ���ݷ����� ����
        ResetAttack();
    }

    private void ResetAttack()
    {
        _stat.Attack = _originalAttackValue;
    }

    protected override void OnHitEvent()
    {
        if (_stat._isStunning && _isAttacking)
        {
            StopLaser(); // ���� �� ���� ���°� �Ǹ� ������ ����
        }

        if (_lockTarget != null)
        {
            // ü��
            Stat targetStat = _lockTarget.GetComponent<Stat>();

            if (targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _stat.AttackRange)
                    State = Define.State.Skill;
                else
                    State = Define.State.Moving;
            }
            else
            {
                State = Define.State.Idle;
            }
        }
        else
        {
            State = Define.State.Idle;
        }
    }
}
