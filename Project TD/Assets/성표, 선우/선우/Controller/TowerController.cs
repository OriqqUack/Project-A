using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    protected TowerStat _towerStat;

    public GameObject bulletPrefab; // �Ҹ� ������
    public Transform fireHead; // Ÿ���� ���� �Ӹ�
    public Transform firePoint; // �Ҹ��� �߻�Ǵ� ����
    public float range = 10f; // Ÿ���� �����Ÿ�
    public float fireRate = 1f; // �߻� �ӵ� (�ʴ� �߻� ��)

    protected float fireCountdown = 0f;

    [SerializeField]
    protected GameObject target;

    [SerializeField]
    protected LayerMask targetLayerMask;

    public bool isDisabled = false;

    protected virtual void Start()
    {
        _towerStat = GetComponent<TowerStat>();
    }

    protected virtual void Update()
    {
        if (isDisabled)
        {
            // Ÿ���� ����� ���峪 �ִ� ����
            return;
        }

        target = UpdateTarget();

        if (target == null)
            return;

        RotateTowardsTarget();
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
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

    protected void RotateTowardsTarget()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - fireHead.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            fireHead.rotation = Quaternion.Lerp(fireHead.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    protected virtual void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, transform);
        BombBullet bullet = bulletGO.GetComponent<BombBullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
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
    }
}
