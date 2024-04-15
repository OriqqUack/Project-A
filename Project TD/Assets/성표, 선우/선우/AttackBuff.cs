using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ݷ� ���� ����
public class AttackBuff : StatusEffect
{
    [SerializeField]
    private int attackIncrease;

    public AttackBuff(float duration, GameObject target, int attackIncrease) : base(duration, target)
    {
        this.attackIncrease = attackIncrease;
    }

    public override void ApplyEffect(MonsterStat monsterStat)
    {
        monsterStat.Attack += attackIncrease;
        Debug.Log("Attack buff applied: +" + attackIncrease + " Attack");
    }

    public override void RemoveEffect(MonsterStat monsterStat)
    {
        monsterStat.Attack -= attackIncrease;
        Debug.Log("Attack buff removed: -" + attackIncrease + " Attack");
    }
}