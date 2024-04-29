using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskTarget : ScriptableObject
{
    public abstract object Value { get;  }

    public abstract bool IsEqual(object target); //target이 같은지 확인하는 작업
}
