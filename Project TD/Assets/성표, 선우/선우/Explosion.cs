using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion
{
    private float explosionRadius;
    private float explosionDamage;

    public Explosion(float radius, float damage) // 생성자
    {
        explosionRadius = radius;
        explosionDamage = damage;
    }

    public void Explode(Stat attacker, Vector3 position)
    {
        // 자폭 효과음 재생 등 필요한 처리 추가

        // 주변에 있는 모든 적에게 피해를 입힘
        Collider[] colliders = Physics.OverlapSphere(position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            // 피해를 입히는 로직을 각자의 스크립트나 컴포넌트에서 처리
            IAttack damageable = collider.GetComponent<Attack>();
            if (damageable != null)
            {
                damageable.OnAttacked(attacker);
            }

        }

        // 자폭 이펙트 생성 등 추가적인 처리
    }
}
