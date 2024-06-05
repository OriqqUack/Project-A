using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(IdleEvent))]
public class Idle : MonoBehaviour
{
    private Rigidbody rb;
    private IdleEvent idleEvent;

    private void Awake()
    {
        // Load component
        rb = GetComponent<Rigidbody>();
        idleEvent = GetComponent<IdleEvent>();
    }

    private void OnEnable()
    {
        // Idle �̺�Ʈ ����
        idleEvent.OnIdle += IdleEvent_OnIdle;
    }

    private void OnDisable()
    {
        // Idle �̺�Ʈ ���� ���
        idleEvent.OnIdle -= IdleEvent_OnIdle;
    }

    private void IdleEvent_OnIdle(IdleEvent idleEvent)
    {
        MoveRigidBody();
    }

    private void MoveRigidBody()
    {
        rb.velocity = Vector3.zero;
    }
}
