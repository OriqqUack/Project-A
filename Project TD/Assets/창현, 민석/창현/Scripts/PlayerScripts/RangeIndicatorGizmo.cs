using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicatorGizmo : MonoBehaviour
{
    public float _attackDistance = 5;
    void Start()
    {
        _attackDistance = GetComponent<PlayerStat>().AttackDistance;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, _attackDistance);
    }
}
