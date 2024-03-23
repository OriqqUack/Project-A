using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAction : MonoBehaviour
{
    private Define.State state;

    private State myState;

    private void Start()
    {
        SetActionType(Define.State.Idle);
    }

    private void Update()
    {
        switch (state)
        {
            case Define.State.Die:
                break;
            case Define.State.Idle:
                break;
            case Define.State.Moving:
                break;
            case Define.State.FallDown:
                break;
            case Define.State.Jump:
                break;
            case Define.State.Attack:
                break;
            case Define.State.Skill:
                break;
        }
    }

    public void SetActionType(Define.State state)
    {
        this.state = state;

        Component component = gameObject.GetComponent<State>() as Component;

        if (component != null)
            Destroy(component);

        switch (state)
        {
            case Define.State.Die:
                myState = gameObject.AddComponent<ConcreteStateDie>();
                myState.DoAction(state);
                break;
            case Define.State.Idle:
                myState = gameObject.AddComponent<ConcreteStateIdle>();
                myState.DoAction(state);
                break;
            case Define.State.Moving:
                myState = gameObject.AddComponent<ConcreteStateMove>();
                myState.DoAction(state);
                break;
            case Define.State.Jump:
                myState = gameObject.AddComponent<ConcreteStateJump>();
                myState.DoAction(state);
                break;
            case Define.State.Attack:
                myState = gameObject.AddComponent<ConcreteStateAttack>();
                myState.DoAction(state);
                break;
        }
    }

    
}
