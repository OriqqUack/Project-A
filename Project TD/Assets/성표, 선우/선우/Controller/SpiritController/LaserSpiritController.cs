using System.Collections;
using UnityEngine;
using PixelArsenal;

public class LaserSpiritController : MonsterController
{
    [SerializeField] private float _damageIncreaseInterval = 3f; // 데미지 증가 간격
    [SerializeField] private float _damageIncreaseAmount = 1f; // 증가하는 피해량
    [SerializeField] private float _laserTickInterval = 0.1f; // 레이저 데미지 틱 간격
    [SerializeField] private float _maxLaserRange; // 레이저 최대 사거리
    [SerializeField] private PixelArsenalBeamStatic _laserBeam; // 레이저 빔 스크립트 참조

    private Coroutine _laserAttackCoroutine;
    private bool _isAttacking = false;
    private GameObject _previousTarget; // 이전 타깃
    private int _originalAttackValue; // 원래의 공격력 값을 저장

    public override void Init()
    {
        base.Init();

        if (_laserBeam == null)
        {
            _laserBeam = GetComponentInChildren<PixelArsenalBeamStatic>();
            if (_laserBeam == null)
            {
                Debug.LogError("PixelArsenalBeamStatic 스크립트가 할당되지 않았습니다.");
            }
        }

        // 빔의 최대 사거리 설정
        _laserBeam.BeamLength = _stat.AttackRange;

        // 원래의 공격력 값을 저장
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

            // 타깃이 변경되었는지 확인
            if (_lockTarget != _previousTarget)
            {
                // 타깃이 변경되었을 경우 공격 초기화
                if (_laserAttackCoroutine != null)
                {
                    StopCoroutine(_laserAttackCoroutine);
                }

                _isAttacking = false;
                _previousTarget = _lockTarget;
            }

            // 빔이 이미 존재하고 활성화된 상태가 아닌 경우에만 공격 시작
            if (!_laserBeam.IsBeamActive)
            {
                _isAttacking = true;
                _laserAttackCoroutine = StartCoroutine(Co_LaserAttack());
            }
        }
    }

    private IEnumerator Co_LaserAttack()
    {
        float currentDamageIncrease = 0f; // 누적 데미지 증가량
        float timeSinceLastTick = 0f; // 0.1초마다 데미지를 주기 위한 타이머
        float timeSinceLastDamageIncrease = 0f; // 3초마다 데미지 증가를 위한 타이머

        // 레이저 빔 스크립트 시작
        _laserBeam.EnableOrSpawnBeam(); // 빔이 있으면 활성화, 없으면 생성

        while (_isAttacking && _lockTarget != null && !_stat._isStunning)
        {
            // 타깃을 향해 레이저를 발사
            _laserBeam.transform.LookAt(_lockTarget.transform.position);

            // 0.1초마다 데미지를 주기 위한 시간 계산
            timeSinceLastTick += Time.deltaTime;
            if (timeSinceLastTick >= _laserTickInterval)
            {
                // 현재 공격력에 추가적인 데미지를 적용
                _stat.Attack = _originalAttackValue + Mathf.FloorToInt(currentDamageIncrease);
                DealDamageToTarget();

                // 타이머 초기화
                timeSinceLastTick = 0f;
            }

            // 3초마다 데미지 증가를 위한 시간 계산
            timeSinceLastDamageIncrease += Time.deltaTime;
            if (timeSinceLastDamageIncrease >= _damageIncreaseInterval)
            {
                currentDamageIncrease += _damageIncreaseAmount;
                timeSinceLastDamageIncrease = 0f; // 타이머 초기화
            }

            // 다음 프레임까지 대기
            yield return null;
        }

        // 공격 후 원래 공격력으로 복구
        _stat.Attack = _originalAttackValue;

        // 레이저 빔 스크립트 종료
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
            // 공격 중 경직 상태가 되면 공격 초기화
            StopCoroutine(_laserAttackCoroutine);
            _isAttacking = false;

            // 공격 후 원래 공격력으로 복구
            _stat.Attack = _originalAttackValue;

            // 레이저 빔 스크립트 종료
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
