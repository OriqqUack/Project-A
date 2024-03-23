using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected Rigidbody _rb;
    protected PlayerStat _stat;
    
    public abstract void DoAction(Define.State state);

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _stat = GetComponent<PlayerStat>();
    }

    protected virtual bool MovingCheck(float horizon, float vertical)
    {
        Vector3 dir = new Vector3(horizon, 0, vertical);

        if (dir.magnitude < 0.1f || (horizon == 0 && vertical == 0))
            return false;
        else
            return true;
    }
}
