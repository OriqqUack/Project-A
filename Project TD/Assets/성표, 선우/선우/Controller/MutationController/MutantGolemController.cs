using System.Collections;
using UnityEngine;

public class MutantGolemController : MonsterController
{
    [SerializeField] private float _attackCooldown = 10f; // 공격 쿨타임
    [SerializeField] private float _wanderRadius = 10f; // 서성거릴 때 이동할 범위
    [SerializeField] private MutantGolemStat _mutantGolemStat;

    private bool _isCharging = false;
    private bool _isAttacking = false;

    public override void Init()
    {
        base.Init();

        _mutantGolemStat = GetComponent<MutantGolemStat>();
        if (_mutantGolemStat == null)
        {
            Debug.LogError("MutantGolemStat이 할당되지 않았습니다.");
        }
    }

    protected override void UpdateSkill()
    {
        if (_stat._isStunning)
        {
            State = Define.State.Stun;
            return;
        }

        if (_isCharging || _isAttacking)
        {
            // 차징 또는 공격 중에는 다른 행동을 하지 않음
            return;
        }

        if (_lockTarget != null)
        {
            RotateTowardsTarget();
        }

        if (_lockTarget != null)
        {
            StartCoroutine(PerformAttack());
        }
        else
        {
            Wander(); // 랜덤으로 서성거림
        }
    }

    // 범위 공격 수행
    private IEnumerator PerformAttack()
    {
        _isCharging = true;
        State = Define.State.Skill;

        // 차징 시간 대기
        yield return new WaitForSeconds(_mutantGolemStat.chargeTime);

        _isCharging = false;
        _isAttacking = true;

        // 범위 내 적 타격
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _mutantGolemStat.attackRadius, LayerMask.GetMask("Player", "Tower"));
        foreach (Collider hitCollider in hitColliders)
        {
            Stat targetStat = hitCollider.GetComponent<Stat>();
            if (targetStat != null)
            {
                targetStat.OnAttacked(_stat); // 범위 내 적에게 데미지 적용
            }
        }

        // 공격 쿨타임 대기
        yield return new WaitForSeconds(_attackCooldown);

        _isAttacking = false;
    }

    // 랜덤 서성거림
    private void Wander()
    {
        State = Define.State.Moving;

        // 일정 범위 내에서 랜덤 위치 지정
        Vector3 randomPosition = new Vector3(
            Random.Range(-_wanderRadius, _wanderRadius),
            transform.position.y,
            Random.Range(-_wanderRadius, _wanderRadius)
        );

        // 지정한 위치로 이동
        MoveToPosition(randomPosition);
    }

    private void MoveToPosition(Vector3 position)
    {
        // 여기에 이동 로직을 추가합니다.
        // 예: NavMeshAgent를 사용하여 지정된 위치로 이동
        // agent.SetDestination(position);
    }

    protected override void UpdateMoving()
    {
        if (_isCharging || _isAttacking || _stat._isStunning)
        {
            return;
        }

        base.UpdateMoving();
    }
}
