using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public GameObject bulletPrefab;             // �Ҹ� ������
    public Transform fireHead;                  // Ÿ���� ���� �Ӹ�
    public Transform firePoint;                 // �Ҹ��� �߻�Ǵ� ����(LV1)
    public float range = 10.0f;                 // Ÿ���� �����Ÿ�
    public float fireRate = 1.0f;               // �߻� �ӵ� (�ʴ� �߻� ��)
    public float cannonRate = 0.6f;             // ���ݱ� �߻� �ӵ�
    public float _repeaterMaxRange = 60.0f;     // ������ �ִ� ����
    public float _repeaterRange = 40.0f;        // ������ �߰� ����
    public float _repeaterMinRange = 20.0f;     // ������ �ּ� ����

    protected TowerStat _towerStat;
    protected float fireCountdown = 0f;

    private float _towerAttack = 0.0f;

    [SerializeField]
    protected GameObject _target;

    [SerializeField]
    protected GameObject _repeater;

    [SerializeField]
    protected LayerMask targetLayerMask;

    [SerializeField]
    protected LayerMask _repeaterLayerMask;

    public bool isDisabled = false;

    protected virtual void Start()
    {
        _towerStat = GetComponent<TowerStat>();
        _towerAttack = _towerStat.Attack;
    }

    protected virtual void Update()
    {
        if (isDisabled)
        {
            // Ÿ���� ����� ���峪 �ִ� ����
            return;
        }

        _target = UpdateTarget();
        _repeater = UpdateRepeaterScan();

        if (_repeater != null)
        {
            StartCoroutine("RepeaterEffect2");
        }
        else
        {
            StopCoroutine("RepeaterEffect2");
            _towerStat.Attack = _towerAttack;
        }

        if (_target != null)
        {
            RotateTowardsTarget();

            if (fireCountdown <= 0f)
            {
                Shoot(bulletPrefab);
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    public virtual GameObject UpdateTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _towerStat.AttackRange, targetLayerMask);
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = collider.gameObject;
            }
        }

        return closestObject;
    }

    public GameObject UpdateRepeaterScan()
    {
        // ����� ���� ���� �ִ��� Ȯ��
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _repeaterMaxRange, _repeaterLayerMask);
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = hitCollider.gameObject;
            }
        }

        return closestObject;
    }

    protected void RotateTowardsTarget()
    {
        Vector3 direction = _target.transform.position - fireHead.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        fireHead.rotation = Quaternion.Lerp(fireHead.rotation, lookRotation, Time.deltaTime * 5f);
    }

    IEnumerator RepeaterEffect2()
    {
        yield return new WaitForSeconds(0.01f);

        float distance = Vector3.Distance(transform.position, _repeater.transform.position);

        if (distance <= _repeaterMaxRange && distance >= _repeaterRange)
        {
            _towerStat.Attack = _towerAttack;
            _towerStat.Attack += _towerStat.Attack * 0.33f;
            StopCoroutine("RepeaterEffect2");
        }
        else if (distance <= _repeaterRange && distance >= _repeaterMinRange)
        {
            _towerStat.Attack = _towerAttack;
            _towerStat.Attack += _towerStat.Attack * 0.66f;
            StopCoroutine("RepeaterEffect2");
        }
        else if (distance <= _repeaterMinRange)
        {
            _towerStat.Attack = _towerAttack;
            _towerStat.Attack += _towerStat.Attack * 1.0f;
            StopCoroutine("RepeaterEffect2");
        }
    }

    protected virtual void Shoot(GameObject _bullet)
    {
        GameObject bulletGO = Instantiate(_bullet, firePoint.position, firePoint.rotation, transform);
        BombBullet bullet = bulletGO.GetComponent<BombBullet>();

        if (bullet != null)
        {
            bullet.Seek(_target);
        }
    }
    public virtual void DisableTower()
    {
        isDisabled = true;
        // Ÿ�� ���� �� �߰� ������ �ʿ��ϸ� ���� �߰�
    }

    public virtual void RepairTower()
    {
        isDisabled = false;
        // Ÿ�� ���� �� �߰� ������ �ʿ��ϸ� ���� �߰�
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _repeaterMinRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _repeaterRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _repeaterMaxRange);
    }
}
