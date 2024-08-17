using System.Collections;
using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    [SerializeField] private float _laserTickInterval = 0.1f; // 0.1�ʸ��� ������
    [SerializeField] private LayerMask _targetLayerMask; // Ÿ�� ���̾� ����ũ
    [SerializeField] private LaserSpiritStat _laserSpiritStat;

    private Coroutine _damageCoroutine;

    private void Start()
    {
        _laserSpiritStat = GetComponentInParent<LaserSpiritStat>();
        if (_laserSpiritStat == null)
        {
            Debug.LogError("LaserSpiritStat ������Ʈ�� �θ� ������Ʈ���� �߰ߵ��� �ʾҽ��ϴ�.");
        }
        _targetLayerMask = LayerMask.GetMask("Player", "Tower");
    }

    public void StartDamageCoroutine()
    {
        if (_damageCoroutine == null)
        {
            _damageCoroutine = StartCoroutine(Co_DamageCoroutine());
        }
    }

    public void StopDamageCoroutine()
    {
        if (_damageCoroutine != null)
        {
            StopCoroutine(_damageCoroutine);
            _damageCoroutine = null;
        }
    }

    private IEnumerator Co_DamageCoroutine()
    {
        while (true)
        {
            DealDamage();
            yield return new WaitForSeconds(_laserTickInterval);
        }
    }

    private void DealDamage()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _laserSpiritStat.AttackRange, _targetLayerMask))
        {
            Stat targetStat = hit.collider.GetComponent<Stat>();
            if (targetStat != null)
            {
                targetStat.OnAttacked(_laserSpiritStat); // LaserSpirit�� Stat�� ����
            }
        }
    }
}
