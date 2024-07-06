using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MutationStat : MonsterStat
{

    protected override void Start()
    {
        SetStat(MonsterName);
        _maxStunCount = 3;
        nextStunHpThreshold = MaxHp * 3 / 4; // 첫번째 경직 기준 체력
    }

    protected override void UpdateNextStunHpThreshold()
    {
        nextStunHpThreshold = MaxHp * (_maxStunCount - _stunCount) / 4;
    }
}
