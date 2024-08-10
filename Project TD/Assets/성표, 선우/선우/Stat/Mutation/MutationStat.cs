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
        _nextStunHpThreshold = MaxHp * 3 / 4; // ù��° ���� ���� ü��
    }

    protected override void UpdateNextStunHpThreshold()
    {
        _nextStunHpThreshold = MaxHp * (_maxStunCount - _stunCount) / 4;
    }
}
