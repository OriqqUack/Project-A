using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region REQUIRE COMPONENTS
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerControl))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(MovementByVelocity))]
[RequireComponent(typeof(MovementToPositionEvent))]
[RequireComponent(typeof(MovementToPosition))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(Idle))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AnimatePlayer))]
[RequireComponent(typeof(Rigidbody))]
[DisallowMultipleComponent]
#endregion REQUIRE COMPONENTS

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerDetailsSO playerDetails;
    [HideInInspector] public Health health;
    [HideInInspector] public MovementByVelocityEvent movementByVelocityEvent;
    [HideInInspector] public MovementToPositionEvent movementToPositionEvent;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public Animator animator;

    private void Awake()
    {
        // load component
        health = GetComponent<Health>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
        animator = GetComponent<Animator>();
        idleEvent = GetComponent<IdleEvent>();
    }

    /// <summary>
    /// �÷��̾� �ʱ�ȭ
    /// </summary>
    /// <param name="playerDetails"></param>
    public void Initalize(PlayerDetailsSO playerDetails)
    {
        this.playerDetails = playerDetails;

        // �÷��̾� ���� ü�� ����
        SetPlayerHealth();
    }

    /// <summary>
    /// playerDetails�� ������ ü���� ������ ü�� ����
    /// </summary>
    private void SetPlayerHealth()
    {
        health.SetStartingHealth(playerDetails.playerHealthAmount);
    }
}
