using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    protected float duration;
    protected GameObject target;


    public float Duration { get { return duration; } }

    public StatusEffect(float duration, GameObject target)
    {
        this.duration = duration;
        this.target = target;
    }

    public abstract int ApplyEffect(int attack);
    public abstract int RemoveEffect(int attack);
}