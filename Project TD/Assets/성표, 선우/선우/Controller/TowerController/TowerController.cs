using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    protected TowerStat _towerStat;

    public GameObject _bulletPrefab; // 불릿 프리팹
    public Transform _fireHead; // 타겟을 따라갈 머리
    public Transform _firePoint; // 불릿이 발사되는 지점
    public float _range = 10f; // 타워의 사정거리
    public float _fireRate = 1f; // 발사 속도 (초당 발사 수)

    protected float _fireCountdown = 0f;

    [SerializeField]
    protected GameObject _target;

    [SerializeField]
    protected LayerMask _targetLayerMask;

    public bool _isDisabled = false;

    protected virtual void Start()
    {
        _towerStat = GetComponent<TowerStat>();
        _targetLayerMask = LayerMask.GetMask("Monster");
    }

    protected virtual void Update()
    {
        if (_isDisabled)
        {
            // 타워의 기능이 고장나 있는 상태
            return;
        }

        _target = UpdateTarget();

        if (_target == null)
            return;

        RotateTowardsTarget();
        if (_fireCountdown <= 0f)
        {
            Shoot();
            _fireCountdown = 1f / _fireRate;
        }

        _fireCountdown -= Time.deltaTime;
    }

    public virtual GameObject UpdateTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _towerStat.AttackRange, _targetLayerMask);
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
        if (_target != null)
        {
            Vector3 direction = _target.transform.position - _fireHead.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _fireHead.rotation = Quaternion.Lerp(_fireHead.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    protected virtual void Shoot()
    {
        GameObject bulletGO = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation, transform);
        BombBullet bullet = bulletGO.GetComponent<BombBullet>();

        if (bullet != null)
        {
            bullet.Seek(_target);
        }
    }

    public virtual void DisableTower()
    {
        _isDisabled = true;
        // 타워 고장 시 추가 로직이 필요하면 여기 추가
    }

    public virtual void RepairTower()
    {
        _isDisabled = false;
        // 타워 수리 시 추가 로직이 필요하면 여기 추가
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
