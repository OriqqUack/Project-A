using System.Collections;
using UnityEngine;

public class PlantController : MonsterController
{
    [SerializeField] private float _infectionRadius = 10.0f; // ���� �ݰ�
    [SerializeField] private GameObject _plant1Prefab; // �Ĺ�1 ������
    [SerializeField] private float _moveCheckInterval = 2f; // ���Ͱ� ���� ������ �̵� üũ ����
    [SerializeField] private float _infectionCooldown = 3f; // ���� ��Ÿ��
    [SerializeField] private GameObject _skullLoopParticlePrefab; // ���� ��ƼŬ ������

    private bool _isInfecting = false; // ���� ������ ����
    private Collider _closestMonster; // ���� ����� ���� ĳ��

    public override void Init()
    {
        base.Init();
        _targetLayerMask = LayerMask.GetMask("Monster"); // ���� ���̾�� ����
    }

    protected override void UpdateMoving()
    {
        // ������ �� �ִ� ���Ͱ� �ִ��� Ȯ��
        _closestMonster = FindClosestInfectableMonster();

        // ���� ����� ������ ���� ���� ��ȯ
        if (_closestMonster != null && !_isInfecting)
        {
            _lockTarget = _closestMonster.gameObject;
            State = Define.State.Skill; // ���� ���� ��ȯ
        }
        else
        {
            // �ֺ��� ���� ����� ������ ������ ���� �������� �̵�
            MoveToDenseArea();
        }
    }

    protected override void UpdateSkill()
    {
        // ���� ������ �� ��ų ������� ����
        if (_stat._isStunning)
        {
            State = Define.State.Stun;
            return;
        }

        // ������ ����� ������ �̵� ���·� ��ȯ
        if (_closestMonster == null || _isInfecting)
        {
            State = Define.State.Moving;
            return;
        }

        // Ÿ���� ���� ȸ��
        RotateTowardsTarget();

        // ���� �ִϸ��̼� �̺�Ʈ�� OnHitEvent ȣ��
    }

    protected override void OnHitEvent()
    {
        // ���� ����� ������ �ƹ��͵� ���� ����
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
        // ������ ���� ����Ͽ� �̵�
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
            if (collider.gameObject == gameObject) // �ڱ� �ڽ� ����
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

        // ���� ó��
        monsterStat.Infect();

        // ������ ���Ϳ� ��ƼŬ �߰�
        if (_skullLoopParticlePrefab != null)
        {
            Instantiate(_skullLoopParticlePrefab, monsterStat.transform.position, Quaternion.identity, monsterStat.transform);
        }

        // �׾��� �� �Ĺ�1 ��ȯ
        monsterStat.OnDeath += () => SpawnPlant1(monsterStat.transform.position);

        // ���� �� ��Ÿ�� ���
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

    // ������ ���� ��ġ ���
    private Vector3 FindMostDenseMonsterArea()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _infectionRadius, _targetLayerMask);
        if (colliders.Length == 0)
            return Vector3.zero;

        Vector3 totalPosition = Vector3.zero;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == gameObject) // �ڱ� �ڽ� ����
                continue;

            totalPosition += collider.transform.position;
        }

        return totalPosition / colliders.Length;
    }
}
