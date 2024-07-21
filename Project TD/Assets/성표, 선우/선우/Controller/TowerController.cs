using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    TowerStat _towerStat;

    public GameObject bulletPrefab; // �Ҹ� ������
    public Transform firePoint; // �Ҹ��� �߻�Ǵ� ����
    public float range = 10f; // Ÿ���� �����Ÿ�
    public float fireRate = 1f; // �߻� �ӵ� (�ʴ� �߻� ��)

    private float fireCountdown = 0f;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    protected LayerMask targetLayerMask;

    public bool isDisabled = false;

    private void Start()
    {
        _towerStat = GetComponent<TowerStat>();
    }

    void Update()
    {
        if (isDisabled)
        {
            // Ÿ���� ����� ���峪 �ִ� ����
            return;
        }

        target = UpdateTarget();

        if (target == null)
            return;

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    public GameObject UpdateTarget()
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

        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //float shortestDistance = Mathf.Infinity;
        //GameObject nearestEnemy = null;

        //foreach (GameObject enemy in enemies)
        //{
        //    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
        //    if (distanceToEnemy < shortestDistance)
        //    {
        //        shortestDistance = distanceToEnemy;
        //        nearestEnemy = enemy;
        //    }
        //}

        //if (nearestEnemy != null && shortestDistance <= range)
        //{
        //    target = nearestEnemy.transform;
        //}
        //else
        //{
        //    target = null;
        //}
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, transform);
        BombBullet bullet = bulletGO.GetComponent<BombBullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    public void DisableTower()
    {
        isDisabled = true;
        // Ÿ�� ���� �� �߰� ������ �ʿ��ϸ� ���� �߰�
    }

    public void RepairTower()
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
