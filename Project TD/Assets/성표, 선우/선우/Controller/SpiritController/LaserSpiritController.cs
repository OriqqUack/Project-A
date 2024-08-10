using System.Collections;
using UnityEngine;
using PixelArsenal;

public class LaserSpiritController : MonsterController
{
    [SerializeField] private float _damageIncreaseInterval = 3f; // ������ ���� ����
    [SerializeField] private float _damageIncreaseAmount = 1f; // �����ϴ� ���ط�
    [SerializeField] private float _laserTickInterval = 0.1f; // ������ ������ ƽ ����
    [SerializeField] private float _maxLaserRange; // ������ �ִ� ��Ÿ�
    [SerializeField] private PixelArsenalBeamStatic _laserBeam; // ������ �� ��ũ��Ʈ ����

    private Coroutine _laserAttackCoroutine;
    private bool _isAttacking = false;
    private GameObject _previousTarget; // ���� Ÿ��
    private int _originalAttackValue; // ������ ���ݷ� ���� ����

    public override void Init()
    {
        base.Init();

        if (_laserBeam == null)
        {
            _laserBeam = GetComponentInChildren<PixelArsenalBeamStatic>();
            if (_laserBeam == null)
            {
                Debug.LogError("PixelArsenalBeamStatic ��ũ��Ʈ�� �Ҵ���� �ʾҽ��ϴ�.");
            }
        }

        // ���� �ִ� ��Ÿ� ����
        _laserBeam.BeamLength = _stat.AttackRange;

        // ������ ���ݷ� ���� ����
        _originalAttackValue = _stat.Attack;
    }

    protected override void UpdateSkill()
    {
        if (_isAttacking || _stat._isStunning)
        {
            return;
        }

        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);

            // Ÿ���� ����Ǿ����� Ȯ��
            if (_lockTarget != _previousTarget)
            {
                // Ÿ���� ����Ǿ��� ��� ���� �ʱ�ȭ
                if (_laserAttackCoroutine != null)
                {
                    StopCoroutine(_laserAttackCoroutine);
                }

                _isAttacking = false;
                _previousTarget = _lockTarget;
            }

            // ���� �̹� �����ϰ� Ȱ��ȭ�� ���°� �ƴ� ��쿡�� ���� ����
            if (!_laserBeam.IsBeamActive)
            {
                _isAttacking = true;
                _laserAttackCoroutine = StartCoroutine(Co_LaserAttack());
            }
        }
    }

    private IEnumerator Co_LaserAttack()
    {
        float currentDamageIncrease = 0f; // ���� ������ ������
        float timeSinceLastTick = 0f; // 0.1�ʸ��� �������� �ֱ� ���� Ÿ�̸�
        float timeSinceLastDamageIncrease = 0f; // 3�ʸ��� ������ ������ ���� Ÿ�̸�

        // ������ �� ��ũ��Ʈ ����
        _laserBeam.EnableOrSpawnBeam(); // ���� ������ Ȱ��ȭ, ������ ����

        while (_isAttacking && _lockTarget != null && !_stat._isStunning)
        {
            // Ÿ���� ���� �������� �߻�
            _laserBeam.transform.LookAt(_lockTarget.transform.position);

            // 0.1�ʸ��� �������� �ֱ� ���� �ð� ���
            timeSinceLastTick += Time.deltaTime;
            if (timeSinceLastTick >= _laserTickInterval)
            {
                // ���� ���ݷ¿� �߰����� �������� ����
                _stat.Attack = _originalAttackValue + Mathf.FloorToInt(currentDamageIncrease);
                DealDamageToTarget();

                // Ÿ�̸� �ʱ�ȭ
                timeSinceLastTick = 0f;
            }

            // 3�ʸ��� ������ ������ ���� �ð� ���
            timeSinceLastDamageIncrease += Time.deltaTime;
            if (timeSinceLastDamageIncrease >= _damageIncreaseInterval)
            {
                currentDamageIncrease += _damageIncreaseAmount;
                timeSinceLastDamageIncrease = 0f; // Ÿ�̸� �ʱ�ȭ
            }

            // ���� �����ӱ��� ���
            yield return null;
        }

        // ���� �� ���� ���ݷ����� ����
        _stat.Attack = _originalAttackValue;

        // ������ �� ��ũ��Ʈ ����
        _laserBeam.DisableBeam();
    }

    private void DealDamageToTarget()
    {
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            if (targetStat != null)
            {
                targetStat.OnAttacked(_stat);
            }
        }
    }

    protected override void OnHitEvent()
    {
        if (_stat._isStunning && _isAttacking)
        {
            // ���� �� ���� ���°� �Ǹ� ���� �ʱ�ȭ
            StopCoroutine(_laserAttackCoroutine);
            _isAttacking = false;

            // ���� �� ���� ���ݷ����� ����
            _stat.Attack = _originalAttackValue;

            // ������ �� ��ũ��Ʈ ����
            if (_laserBeam != null && _laserBeam.IsBeamActive)
            {
                _laserBeam.DisableBeam();
            }
        }

        base.OnHitEvent();
    }

    protected override void UpdateMoving()
    {
        if (_isAttacking || _stat._isStunning)
        {
            return;
        }

        base.UpdateMoving();
    }
}
