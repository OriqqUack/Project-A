using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public GameObject bulletPrefab;             // 불릿 프리팹
    public Transform fireHead;                  // 타겟을 따라갈 머리
    public Transform firePoint;                 // 불릿이 발사되는 지점(LV1)
    public float range = 10.0f;                 // 타워의 사정거리
    public float fireRate = 1.0f;               // 발사 속도 (초당 발사 수)
    public float cannonRate = 0.6f;             // 폭격기 발사 속도
    public float _repeaterMaxRange = 60.0f;     // 리피터 최대 범위
    public float _repeaterRange = 40.0f;        // 리피터 중간 범위
    public float _repeaterMinRange = 20.0f;     // 리피터 최소 범위

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
            // 타워의 기능이 고장나 있는 상태
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
        // 대상이 범위 내에 있는지 확인
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
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _repeaterMinRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _repeaterRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _repeaterMaxRange);
    }
}
