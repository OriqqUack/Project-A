using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect
{
    protected float duration;
    protected GameObject target;


    public float Duration { get { return duration; } }

    public StatusEffect(float duration, GameObject target)
    {
        this.duration = duration;
        this.target = target;
    }

    public abstract void ApplyEffect(MonsterStat monsterStat);
    public abstract void RemoveEffect(MonsterStat monsterStat);
}