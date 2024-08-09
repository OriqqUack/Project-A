using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HealingSpiritController : MonsterController
{
    [SerializeField]
    private float _healingRadius = 5f; // 힐 범위
    [SerializeField]
    private float _healingCooldown = 2f; // 힐 쿨타임
    [SerializeField]
    private int _healingAmount = 10; // 힐 하는 정도

    private bool _isHealing = false; // 힐 중인지
    private bool _isFleeing = false; // 데미지를 받아 도망중인지

    [SerializeField]
    LayerMask _healTarget; // 힐 받는 레이어

    public override void Init()
    {
        base.Init();
        _healTarget = LayerMask.GetMask("Monster"); // 힐 받는 레이어를 몬스터로 설정
    }

    protected override void UpdateMoving()
    {
        if (_isFleeing)
        {
            // 도망 상태에서는 별도의 행동을 하지 않음
            return;
        }

        // 기본 이동 처리
        base.UpdateMoving();
    }

    protected override void UpdateSkill()
    {
        if (_isHealing)
            return;

        if (_stat._isStunning)
        {
            State = Define.State.Stun;
            return;
        }

        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    // 힐 하는 코루틴
    private IEnumerator Co_HealNearbyAllies()
    {
        _isHealing = true;
        // 힐 동작 수행
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _healingRadius, _healTarget);
        foreach (var hitCollider in hitColliders)
        {
            MonsterStat monsterStat = hitCollider.GetComponent<MonsterStat>();
            if (monsterStat != null)
            {
                monsterStat.Hp = Mathf.Min(monsterStat.MaxHp, monsterStat.Hp + _healingAmount);
            }
        }

        Debug.Log("Healing");
        // 힐 쿨다운
        yield return new WaitForSeconds(_healingCooldown);
        _isHealing = false;
    }

    // 외부에서 호출하여 도망가는 행동을 트리거하는 메서드
    public void TriggerFlee()
    {
        if (_isFleeing || _stat.Hp <= 0)
            return;

        StartCoroutine(Co_FleeCoroutine());
        Debug.Log("Flee");
    }

    // 피해를 입었을 때 타깃의 반대로 도망치는 코루틴
    private IEnumerator Co_FleeCoroutine()
    {
        _isFleeing = true;
        State = Define.State.Moving;
        _navMeshAgent.speed = _stat.MoveSpeed;
        Vector3 fleeDirection = (transform.position - _lockTarget.transform.position).normalized;
        _navMeshAgent.SetDestination(transform.position + fleeDirection * 5f); // 도망가는 거리
        yield return new WaitForSeconds(1f);
        _isFleeing = false;
        State = Define.State.Idle;
    }

    // 공격 애니메이션이 발동될때 힐이 시작되도록 설정
    protected override void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            // 힐을 시작
            StartCoroutine(Co_HealNearbyAllies());
        }

        base.OnHitEvent();
    }


    // 힐 범위를 기즈모로 표시
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // 기즈모 색상을 녹색으로 설정
        Gizmos.DrawWireSphere(transform.position, _healingRadius); // 힐 범위 원 그리기
    }
}
