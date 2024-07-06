using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MutationStat : MonsterStat
{
    protected new int _maxStunCount = 3; // �������̴� ���� �� 4��

    protected override void Start()
    {
        base.Start();
        nextStunHpThreshold = MaxHp * 3 / 4; // ù��° ���� ���� ü��
    }

    protected override void UpdateNextStunHpThreshold()
    {
        nextStunHpThreshold = MaxHp * (_maxStunCount - _stunCount) / 4;
    }
}
