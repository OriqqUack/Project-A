using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuicideBomberController : MonsterController
{
    private float lifeTime = 4.0f; // 자폭병 생존 시간
    private float explosionRadius = 4.0f; // 자폭 범위
    private int explosionDamage = 50; // 자폭 데미지

    public event System.Action OnDespawn; // 자폭병이 소멸될 때 이벤트

    [SerializeField]
    private LayerMask targetLayerMask; // 자폭병이 데미지를 줄 타깃레이어마스크

    public override void Init()
    {
        base.Init();
        StartCoroutine(SelfDestruct());
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(lifeTime);
        Explode();
    }

    private void Explode()
    {
        // 자폭 로직: 일정 범위 내에 있는 적에게 데미지를 입힘
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, targetLayerMask);
        foreach (var collider in colliders)
        {
            Stat targetStat = collider.GetComponent<Stat>();
            if (targetStat != null && collider.gameObject != gameObject)
            {
                targetStat.OnAttacked(new MonsterStat { Attack = explosionDamage });
            }
        }
        // 자폭 후 소멸
        Destroy(gameObject);
        OnDespawn?.Invoke();
    }

    
}
