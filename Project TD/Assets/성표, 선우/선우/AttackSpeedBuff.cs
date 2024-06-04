using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 공격속도 증가 버프
public class AttackSpeedBuff : StatusEffect
{
    [SerializeField]
    private int attackSpeedIncrease;

    public AttackSpeedBuff(float duration, GameObject target, int attackSpeedIncrease) : base(duration, target)
    {
        this.attackSpeedIncrease = attackSpeedIncrease;
    }

    public override void ApplyEffect(MonsterStat monsterStat)
    {
        monsterStat.AttackSpeed += attackSpeedIncrease;
        Debug.Log("Attack buff applied: +" + attackSpeedIncrease + " Attack");
    }

    public override void RemoveEffect(MonsterStat monsterStat)
    {
        monsterStat.AttackSpeed -= attackSpeedIncrease;
        Debug.Log("Attack buff removed: -" + attackSpeedIncrease + " Attack");
    }
}