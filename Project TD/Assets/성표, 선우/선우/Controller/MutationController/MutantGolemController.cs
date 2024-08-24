using System.Collections;
using UnityEngine;

public class MutantGolemController : MonsterController
{
    [SerializeField] private float _attackCooldown = 10f; // ���� ��Ÿ��
    [SerializeField] private float _wanderRadius = 10f; // �����Ÿ� �� �̵��� ����
    [SerializeField] private MutantGolemStat _mutantGolemStat;

    private bool _isCharging = false;
    private bool _isAttacking = false;

    public override void Init()
    {
        base.Init();

        _mutantGolemStat = GetComponent<MutantGolemStat>();
        if (_mutantGolemStat == null)
        {
            Debug.LogError("MutantGolemStat�� �Ҵ���� �ʾҽ��ϴ�.");
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
            // ��¡ �Ǵ� ���� �߿��� �ٸ� �ൿ�� ���� ����
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
            Wander(); // �������� �����Ÿ�
        }
    }

    // ���� ���� ����
    private IEnumerator PerformAttack()
    {
        _isCharging = true;
        State = Define.State.Skill;

        // ��¡ �ð� ���
        yield return new WaitForSeconds(_mutantGolemStat.chargeTime);

        _isCharging = false;
        _isAttacking = true;

        // ���� �� �� Ÿ��
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _mutantGolemStat.attackRadius, LayerMask.GetMask("Player", "Tower"));
        foreach (Collider hitCollider in hitColliders)
        {
            Stat targetStat = hitCollider.GetComponent<Stat>();
            if (targetStat != null)
            {
                targetStat.OnAttacked(_stat); // ���� �� ������ ������ ����
            }
        }

        // ���� ��Ÿ�� ���
        yield return new WaitForSeconds(_attackCooldown);

        _isAttacking = false;
    }

    // ���� �����Ÿ�
    private void Wander()
    {
        State = Define.State.Moving;

        // ���� ���� ������ ���� ��ġ ����
        Vector3 randomPosition = new Vector3(
            Random.Range(-_wanderRadius, _wanderRadius),
            transform.position.y,
            Random.Range(-_wanderRadius, _wanderRadius)
        );

        // ������ ��ġ�� �̵�
        MoveToPosition(randomPosition);
    }

    private void MoveToPosition(Vector3 position)
    {
        // ���⿡ �̵� ������ �߰��մϴ�.
        // ��: NavMeshAgent�� ����Ͽ� ������ ��ġ�� �̵�
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
