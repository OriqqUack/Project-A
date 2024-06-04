using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuicideBomberController : MonsterController
{
    private float lifeTime = 4.0f; // ������ ���� �ð�
    private float explosionRadius = 4.0f; // ���� ����
    private int explosionDamage = 50; // ���� ������

    public event System.Action OnDespawn; // �������� �Ҹ�� �� �̺�Ʈ

    [SerializeField]
    private LayerMask targetLayerMask; // �������� �������� �� Ÿ�극�̾��ũ

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
        // ���� ����: ���� ���� ���� �ִ� ������ �������� ����
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, targetLayerMask);
        foreach (var collider in colliders)
        {
            Stat targetStat = collider.GetComponent<Stat>();
            if (targetStat != null && collider.gameObject != gameObject)
            {
                targetStat.OnAttacked(new MonsterStat { Attack = explosionDamage });
            }
        }
        // ���� �� �Ҹ�
        Destroy(gameObject);
        OnDespawn?.Invoke();
    }

    
}
