using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour
{
    [SerializeField]
    private TowerStat _towerStat;

    [SerializeField]
    private GameObject _target;
    public float _speed = 70f;

    private void Start()
    {
        Transform parentTransform = transform.parent;
        if (parentTransform != null)
        {
            _towerStat = parentTransform.GetComponent<TowerStat>();
            if (_towerStat == null)
            {
                Debug.LogError("TowerStat component not found on parent");
            }
        }
        else
        {
            Debug.LogError("Parent transform not found");
        }
    }

    public void Seek(GameObject target)
    {
        _target = target;
    }

    void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = _target.transform.position - transform.position;
        float distanceThisFrame = _speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(_target.transform);
    }

    void HitTarget()
    {
        // 여기에 적에게 데미지를 주는 로직을 구현
        Damage();
        Destroy(gameObject);
    }

    void Damage()
    {
        Stat targetStat = _target.GetComponent<Stat>();

        if (targetStat != null)
        {
            targetStat.OnAttacked(_towerStat);
        }
    }
}
