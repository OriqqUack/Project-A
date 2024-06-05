using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

[RequireComponent(typeof(Rigidbody))]
[RequiredInterface(typeof(MovementByVelocityEvent))]
public class MovementToPosition : MonoBehaviour
{
    private Rigidbody rb;
    private MovementToPositionEvent movementToPositionEvent;

    private void Awake()
    {
        // load component
        rb = GetComponent<Rigidbody>();
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
    }

    private void OnEnable()
    {
        // �뽬(������ġ �̵�) �̺�Ʈ ����
        movementToPositionEvent.OnMovementToPosition += MovementToPositionEvent_OnMovementToPosition;
    }

    private void OnDisable()
    {
        // �뽬(������ġ �̵�) �̺�Ʈ ���� ���
        movementToPositionEvent.OnMovementToPosition -= MovementToPositionEvent_OnMovementToPosition;
    }

    // �뽬 �̺�Ʈ �Լ�
    private void MovementToPositionEvent_OnMovementToPosition(MovementToPositionEvent movementToPositionEvent, MovementToPositionArgs movementToPositionArgs)
    {
        MoveRigidBody(movementToPositionArgs.movePosition, movementToPositionArgs.currentPosition, movementToPositionArgs.moveSpeed);
    }

    /// <summary>
    /// RigidBody �̵� �Լ�
    /// </summary>
    /// <param name="movePosition"></param>
    /// <param name="currentPosition"></param>
    /// <param name="moveSpeed"></param>
    private void MoveRigidBody(Vector3 movePosition, Vector3 currentPosition, float moveSpeed)
    {
        Vector3 unitVector = Vector3.Normalize(movePosition - currentPosition);

        rb.MovePosition(rb.position + (unitVector * moveSpeed * Time.fixedDeltaTime));
    }
}
