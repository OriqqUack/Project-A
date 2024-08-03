using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    protected TowerStat _towerStat;

    public GameObject bulletPrefab; // 불릿 프리팹
    public Transform fireHead; // 타겟을 따라갈 머리
    public Transform firePoint; // 불릿이 발사되는 지점
    public float range = 10f; // 타워의 사정거리
    public float fireRate = 1f; // 발사 속도 (초당 발사 수)

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
            // 타워의 기능이 고장나 있는 상태
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
        // 타워 고장 시 추가 로직이 필요하면 여기 추가
    }

    public virtual void RepairTower()
    {
        isDisabled = false;
        // 타워 수리 시 추가 로직이 필요하면 여기 추가
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
