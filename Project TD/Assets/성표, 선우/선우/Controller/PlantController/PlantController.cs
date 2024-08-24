using System.Collections;
using UnityEngine;

public class PlantController : MonsterController
{
    [SerializeField] private float _infectionRadius = 10.0f; // 감염 반경
    [SerializeField] private GameObject _plant1Prefab; // 식물1 프리팹
    [SerializeField] private float _moveCheckInterval = 2f; // 몬스터가 많은 곳으로 이동 체크 간격
    [SerializeField] private float _infectionCooldown = 3f; // 감염 쿨타임
    [SerializeField] private GameObject _skullLoopParticlePrefab; // 감염 파티클 프리팹

    private bool _isInfecting = false; // 감염 중인지 여부
    private Collider _closestMonster; // 가장 가까운 몬스터 캐싱

    public override void Init()
    {
        base.Init();
        _targetLayerMask = LayerMask.GetMask("Monster"); // 몬스터 레이어로 설정
    }

    protected override void UpdateMoving()
    {
        // 감염할 수 있는 몬스터가 있는지 확인
        _closestMonster = FindClosestInfectableMonster();

        // 감염 대상이 있으면 감염 모드로 전환
        if (_closestMonster != null && !_isInfecting)
        {
            _lockTarget = _closestMonster.gameObject;
            State = Define.State.Skill; // 감염 모드로 전환
        }
        else
        {
            // 주변에 감염 대상이 없으면 밀집된 몬스터 구역으로 이동
            MoveToDenseArea();
        }
    }

    protected override void UpdateSkill()
    {
        // 경직 상태일 때 스킬 사용하지 않음
        if (_stat._isStunning)
        {
            State = Define.State.Stun;
            return;
        }

        // 감염할 대상이 없으면 이동 상태로 전환
        if (_closestMonster == null || _isInfecting)
        {
            State = Define.State.Moving;
            return;
        }

        // 타깃을 향해 회전
        RotateTowardsTarget();

        // 감염 애니메이션 이벤트로 OnHitEvent 호출
    }

    protected override void OnHitEvent()
    {
        // 감염 대상이 없으면 아무것도 하지 않음
        if (_closestMonster == null)
        {
            State = Define.State.Moving;
            return;
        }

        MonsterStat targetStat = _closestMonster.GetComponent<MonsterStat>();
        if (targetStat != null && !targetStat.IsInfected)
        {
            StartCoroutine(InfectMonster(targetStat));
        }
    }

    private void MoveToDenseArea()
    {
        // 밀집된 구역 계산하여 이동
        Vector3 densePosition = FindMostDenseMonsterArea();
        if (densePosition != Vector3.zero)
        {
            _destPos = densePosition;
            _navMeshAgent.SetDestination(_destPos);
        }
    }

    private Collider FindClosestInfectableMonster()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _infectionRadius, _targetLayerMask);
        Collider closestCollider = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == gameObject) // 자기 자신 제외
                continue;

            MonsterStat monsterStat = collider.GetComponent<MonsterStat>();
            if (monsterStat != null && !monsterStat.IsInfected)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCollider = collider;
                }
            }
        }
        return closestCollider;
    }

    private IEnumerator InfectMonster(MonsterStat monsterStat)
    {
        _isInfecting = true;

        // 감염 처리
        monsterStat.Infect();

        // 감염된 몬스터에 파티클 추가
        if (_skullLoopParticlePrefab != null)
        {
            Instantiate(_skullLoopParticlePrefab, monsterStat.transform.position, Quaternion.identity, monsterStat.transform);
        }

        // 죽었을 때 식물1 소환
        monsterStat.OnDeath += () => SpawnPlant1(monsterStat.transform.position);

        // 감염 후 쿨타임 대기
        yield return new WaitForSeconds(_infectionCooldown);

        _isInfecting = false;
        State = Define.State.Moving;
    }

    private void SpawnPlant1(Vector3 position)
    {
        if (_plant1Prefab != null)
        {
            Instantiate(_plant1Prefab, position, Quaternion.identity);
        }
    }

    // 밀집된 몬스터 위치 계산
    private Vector3 FindMostDenseMonsterArea()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _infectionRadius, _targetLayerMask);
        if (colliders.Length == 0)
            return Vector3.zero;

        Vector3 totalPosition = Vector3.zero;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == gameObject) // 자기 자신 제외
                continue;

            totalPosition += collider.transform.position;
        }

        return totalPosition / colliders.Length;
    }
}
