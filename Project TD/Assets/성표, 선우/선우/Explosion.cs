using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion
{
    private float explosionRadius;
    private float explosionDamage;

    public Explosion(float radius, float damage) // ������
    {
        explosionRadius = radius;
        explosionDamage = damage;
    }

    public void Explode(Stat attacker, Vector3 position)
    {
        // ���� ȿ���� ��� �� �ʿ��� ó�� �߰�

        // �ֺ��� �ִ� ��� ������ ���ظ� ����
        Collider[] colliders = Physics.OverlapSphere(position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            // ���ظ� ������ ������ ������ ��ũ��Ʈ�� ������Ʈ���� ó��
            IAttack damageable = collider.GetComponent<Attack>();
            if (damageable != null)
            {
                damageable.OnAttacked(attacker);
            }

        }

        // ���� ����Ʈ ���� �� �߰����� ó��
    }
}
