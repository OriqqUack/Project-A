using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MutationStat : MonsterStat
{
    protected new int _maxStunCount = 3; // 돌연변이는 경직 총 4번

    protected override void Start()
    {
        base.Start();
        nextStunHpThreshold = MaxHp * 3 / 4; // 첫번째 경직 기준 체력
    }

    protected override void UpdateNextStunHpThreshold()
    {
        nextStunHpThreshold = MaxHp * (_maxStunCount - _stunCount) / 4;
    }
}
