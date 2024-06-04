using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteStateFallDown : State
{
    float coolTime = 0.6f;
    float staindUpCoolTime = 5f;
    public override void DoAction(Define.State state)
    {
        CoroutineRunningCheck();
        _coroutine = StartCoroutine(FallDown());
    }

    IEnumerator FallDown()
    {
        float elapsedTime = 0;
        while (elapsedTime < coolTime)
        {
            _rb.velocity = transform.forward * _stat.MoveSpeed;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(staindUpCoolTime);
        _anim.SetBool("isMoving", false);
        _anim.SetBool("isTripping", false);
        GetComponent<MyAction>().SetActionType(Define.State.Idle);
    }
}
