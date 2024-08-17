using System.Collections;
using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    [SerializeField] private float _laserTickInterval = 0.1f; // 0.1초마다 데미지
    [SerializeField] private LayerMask _targetLayerMask; // 타겟 레이어 마스크
    [SerializeField] private LaserSpiritStat _laserSpiritStat;

    private Coroutine _damageCoroutine;

    private void Start()
    {
        _laserSpiritStat = GetComponentInParent<LaserSpiritStat>();
        if (_laserSpiritStat == null)
        {
            Debug.LogError("LaserSpiritStat 컴포넌트가 부모 오브젝트에서 발견되지 않았습니다.");
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
                targetStat.OnAttacked(_laserSpiritStat); // LaserSpirit의 Stat을 전달
            }
        }
    }
}
