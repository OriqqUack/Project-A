using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClassStatEffect
{
    protected GameObject target;

    public ClassStatEffect(GameObject target)
    {
        this.target = target;
    }

    public abstract void ApplyStat();
    public abstract void RemoveStat();
}
