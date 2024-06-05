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
        // �̵� �̺�Ʈ ����
        movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
    }

    private void OnDisable()
    {
        // �̵� �̺�Ʈ ���� ���
        movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
    }

    // �̵� �̺�Ʈ �Լ�
    private void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent,
        MovementByVelocityArgs movementByVelocityArgs)
    {
        MoveRigidBody(movementByVelocityArgs.moveDirection, movementByVelocityArgs.moveSpeed);
    }

    private void MoveRigidBody(Vector3 moveDirection, float moveSpeed)
    {
        //�̵� �ӵ� ����
        rb.velocity = moveDirection * moveSpeed;
    }
}
