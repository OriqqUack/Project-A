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

    public override int ApplyEffect(int attack)
    {
        attack += attackIncrease;
        Debug.Log("Attack buff applied: +" + attackIncrease + " Attack");
        return attack;
    }

    public override int RemoveEffect(int attack)
    {
        attack -= attackIncrease;
        Debug.Log("Attack buff removed: -" + attackIncrease + " Attack");
        return attack;
    }
}