using System.Collections;
using UnityEngine;
using PixelArsenal;

public class LaserSpiritController : SpiritController
{
    [SerializeField] private float _damageIncreaseInterval = 3f; // 데미지 증가 간격
    [SerializeField] private float _damageIncreaseAmount = 1f; // 증가하는 피해량
    [SerializeField] private PixelArsenalBeamStatic _laserBeam; // 레이저 빔 스크립트 참조
    [SerializeField] private LaserDamage _laserDamage; // Co_DamageCoroutine를 제어하기 위해 참조

    private Coroutine _laserAttackCoroutine;
    private bool _isAttacking = false;
    private GameObject _previousTarget; // 이전 타깃
    private int _originalAttackValue; // 원래의 공격력 값을 저장

    public override void Init()
    {
        base.Init();

        _laserDamage = GetComponentInChildren<LaserDamage>();

        if (_laserBeam == null)
        {
            Debug.LogError("레이저 빔 스크립트가 연결되지 않았습니다.");
            return;
        }

        _laserBeam.BeamLength = _stat.AttackRange; // 빔의 최대 사거리 설정
        _originalAttackValue = _stat.Attack; // 원래의 공격력 값을 저장
        _laserBeam.Initialize(transform);  // LaserSpirit의 Transform을 전달
    }

    protected override void UpdateSkill()
    {
        if (_stat._isStunning)
        {
            State = Define.State.Stun;
            StopLaser(); // 레이저 중지
            return;
        }

        if (_lockTarget != null)
        {
            // 타깃이 변경되었는지 확인
            if (_lockTarget != _previousTarget)
            {
                if (_laserAttackCoroutine != null)
                {
                    StopCoroutine(_laserAttackCoroutine);
                }

                StopLaser(); // 레이저 중지
                _isAttacking = false;
                _previousTarget = _lockTarget;
            }

            // 공격중이 아니고 빔이 활성화된 상태가 아닌 경우에만 공격 시작
            if (!_isAttacking && !_laserBeam.IsBeamActive)
            {
                _isAttacking = true;
                _laserAttackCoroutine = StartCoroutine(Co_LaserAttack());
            }
        }

        RotateTowardsTarget(); // 타깃을 향해 회전
    }

    protected override void UpdateMoving()
    {
        if (_isAttacking)
        {
            StopLaser(); // 레이저 중지
            return;
        }

        if (_stat._isStunning)
        {
            State = Define.State.Stun;
            StopLaser(); // 레이저 중지
            return;
        }

        base.UpdateMoving();
    }

    private IEnumerator Co_LaserAttack()
    {
        float currentDamageIncrease = 0f;
        float timeSinceLastDamageIncrease = 0f;

        // 레이저 빔 스크립트 시작
        _laserBeam.EnableBeam();

        // 레이저의 위치를 정령 위치로 설정
        _laserBeam.transform.position = transform.position;
        _laserDamage.StartDamageCoroutine();

        while (_isAttacking && _lockTarget != null && !_stat._isStunning)
        {
            _laserBeam.transform.LookAt(_lockTarget.transform.position); // 타깃을 향해 레이저를 발사

            // 3초마다 데미지 증가를 위한 시간 계산
            timeSinceLastDamageIncrease += Time.deltaTime;
            if (timeSinceLastDamageIncrease >= _damageIncreaseInterval)
            {
                currentDamageIncrease += _damageIncreaseAmount;
                _stat.Attack = _originalAttackValue + Mathf.FloorToInt(currentDamageIncrease);
                timeSinceLastDamageIncrease = 0f; // 타이머 초기화
            }

            // 다음 프레임까지 대기
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

        // 공격 후 원래 공격력으로 복구
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
            StopLaser(); // 공격 중 경직 상태가 되면 레이저 중지
        }

        if (_lockTarget != null)
        {
            // 체력
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
