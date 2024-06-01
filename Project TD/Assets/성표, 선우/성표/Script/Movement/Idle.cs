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
        // Idle 이벤트 구독
        idleEvent.OnIdle += IdleEvent_OnIdle;
    }

    private void OnDisable()
    {
        // Idle 이벤트 구독 취소
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
