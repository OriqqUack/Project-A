using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteStateMove : State
{
    float InputX;
    float InputZ;

    float fallDownPer = 0.001f;
    float rotationSpeed = 10.0f;
    public override void DoAction(Define.State state)
    {
        CoroutineRunningCheck();
        _coroutine = StartCoroutine("Move");
    }

    IEnumerator Move()
    {
        while (true)
        {
            InputX = Input.GetAxisRaw("Horizontal");
            InputZ = Input.GetAxisRaw("Vertical");
            Vector3 dir = new Vector3(InputX, 0, InputZ).normalized;

            //�Ѿ����� üũ
            if (IsFallDown())
            {
                yield return new WaitForSeconds(0.1f);
                _anim.SetBool("isTripping", true);
                GetComponent<MyAction>().SetActionType(Define.State.FallDown);
                break;
            }

            bool _isMoving = MovingCheck(InputX, InputZ);
            if (!_isMoving)
            {
                _rb.velocity = Vector3.zero;
                GetComponent<MyAction>().SetActionType(Define.State.Idle);
                break;
            }

            _rb.velocity = dir * _stat.MoveSpeed;
            Quaternion targetRotation = Quaternion.LookRotation(dir);

            // �ε巯�� ȸ���� ���� Slerp �Լ��� ����մϴ�.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        
    }

    private bool IsFallDown()
    {
        float randomNumber = Random.value;
        if (randomNumber < fallDownPer)
            return true;
        return false;
    }
}
