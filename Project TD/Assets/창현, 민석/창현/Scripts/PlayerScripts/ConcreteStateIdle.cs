using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteStateIdle : State
{
    float _hAxis;
    float _vAxis;
    public override void DoAction(Define.State state)
    {
        CoroutineRunningCheck();
    }

    private void Update()
    {
        _hAxis = Input.GetAxisRaw("Horizontal");
        _vAxis = Input.GetAxisRaw("Vertical");

        bool _isMoving = MovingCheck(_hAxis, _vAxis);
        if (_isMoving)
        {
            GetComponent<MyAction>().SetActionType(Define.State.Moving);
        }
    }


    
}

