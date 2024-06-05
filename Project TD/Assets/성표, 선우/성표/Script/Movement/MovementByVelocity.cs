using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MovementByVelocityEvent))]
public class MovementByVelocity : MonoBehaviour
{
    private Rigidbody rb;
    private MovementByVelocityEvent movementByVelocityEvent;

    private void Awake()
    {
        // load component
        rb = GetComponent<Rigidbody>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
    }

    private void OnEnable()
    {
        // 이동 이벤트 구독
        movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
    }

    private void OnDisable()
    {
        // 이동 이벤트 구독 취소
        movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
    }

    // 이동 이벤트 함수
    private void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent,
        MovementByVelocityArgs movementByVelocityArgs)
    {
        MoveRigidBody(movementByVelocityArgs.moveDirection, movementByVelocityArgs.moveSpeed);
    }

    private void MoveRigidBody(Vector3 moveDirection, float moveSpeed)
    {
        //이동 속도 설정
        rb.velocity = moveDirection * moveSpeed;
    }
}
