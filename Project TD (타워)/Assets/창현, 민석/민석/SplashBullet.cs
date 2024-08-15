using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashBullet : BombBullet
{
    public float radius = 5f;       // ���÷��� ����
    public LayerMask targetLayer;   // �������� ���� ����� ���̾�

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void HitTarget()
    {
        // ���⿡ ������ �������� �ִ� ������ ����
        Damage();
        Destroy(gameObject);
    }

    public override void Damage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, targetLayer);
        foreach (Collider hitCollider in hitColliders)
        {
            Stat targetStat = hitCollider.GetComponent<Stat>();
            if (targetStat != null)
            {
                targetStat.OnAttacked(_towerStat);
            }
        }
    }
}
