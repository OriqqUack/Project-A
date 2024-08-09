using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HealingSpiritController : MonsterController
{
    [SerializeField]
    private float _healingRadius = 5f; // �� ����
    [SerializeField]
    private float _healingCooldown = 2f; // �� ��Ÿ��
    [SerializeField]
    private int _healingAmount = 10; // �� �ϴ� ����

    private bool _isHealing = false; // �� ������
    private bool _isFleeing = false; // �������� �޾� ����������

    [SerializeField]
    LayerMask _healTarget; // �� �޴� ���̾�

    public override void Init()
    {
        base.Init();
        _healTarget = LayerMask.GetMask("Monster"); // �� �޴� ���̾ ���ͷ� ����
    }

    protected override void UpdateMoving()
    {
        if (_isFleeing)
        {
            // ���� ���¿����� ������ �ൿ�� ���� ����
            return;
        }

        // �⺻ �̵� ó��
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

    // �� �ϴ� �ڷ�ƾ
    private IEnumerator Co_HealNearbyAllies()
    {
        _isHealing = true;
        // �� ���� ����
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
        // �� ��ٿ�
        yield return new WaitForSeconds(_healingCooldown);
        _isHealing = false;
    }

    // �ܺο��� ȣ���Ͽ� �������� �ൿ�� Ʈ�����ϴ� �޼���
    public void TriggerFlee()
    {
        if (_isFleeing || _stat.Hp <= 0)
            return;

        StartCoroutine(Co_FleeCoroutine());
        Debug.Log("Flee");
    }

    // ���ظ� �Ծ��� �� Ÿ���� �ݴ�� ����ġ�� �ڷ�ƾ
    private IEnumerator Co_FleeCoroutine()
    {
        _isFleeing = true;
        State = Define.State.Moving;
        _navMeshAgent.speed = _stat.MoveSpeed;
        Vector3 fleeDirection = (transform.position - _lockTarget.transform.position).normalized;
        _navMeshAgent.SetDestination(transform.position + fleeDirection * 5f); // �������� �Ÿ�
        yield return new WaitForSeconds(1f);
        _isFleeing = false;
        State = Define.State.Idle;
    }

    // ���� �ִϸ��̼��� �ߵ��ɶ� ���� ���۵ǵ��� ����
    protected override void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            // ���� ����
            StartCoroutine(Co_HealNearbyAllies());
        }

        base.OnHitEvent();
    }


    // �� ������ ������ ǥ��
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // ����� ������ ������� ����
        Gizmos.DrawWireSphere(transform.position, _healingRadius); // �� ���� �� �׸���
    }
}
