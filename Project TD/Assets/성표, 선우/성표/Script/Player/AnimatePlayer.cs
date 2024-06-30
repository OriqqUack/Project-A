using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class AnimatePlayer : MonoBehaviour
{
    private Player player;
    private MovementByVelocity movementByVelocity;

    private void Awake()
    {
        // Load component
        player = GetComponent<Player>();
        movementByVelocity = GetComponent<MovementByVelocity>();
    }

    private void OnEnable()
    {
        // Subscribe to movement by velocity event
        player.movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;

        // Subscribe to movement to postion event
        player.movementToPositionEvent.OnMovementToPosition += MovementToPositionEvent_OnMovementToPosition;

        // Subscribe to idle event
        player.idleEvent.OnIdle += IdleEvent_OnIdle;

    }

    private void OnDisable()
    {
        // Unsubscribe to movement by velocity event
        player.movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;

        // Unsubscribe to movement to postion event
        player.movementToPositionEvent.OnMovementToPosition -= MovementToPositionEvent_OnMovementToPosition;

        // Unsubscrible to idle event
        player.idleEvent.OnIdle -= IdleEvent_OnIdle;
    }

    private void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent,
       MovementByVelocityArgs movementByVelocityArgs)
    {
        InitalizeRollAnimationParameters();
        SetMovementAnimationParameters();
    }

    private void MovementToPositionEvent_OnMovementToPosition(MovementToPositionEvent movementToPositionEvent,
      MovementToPositionArgs movementToPositionArgs)
    {
        SetMovementAnimationParameters(movementToPositionArgs);
    }
        
    private void IdleEvent_OnIdle(IdleEvent idleEvent)
    {
        InitalizeRollAnimationParameters();
        SetIdleAnimationParameters();
    }

    private void SetMovementAnimationParameters()
    {
        player.animator.SetBool(Settings.isMoving, true);
        player.animator.SetBool(Settings.isIdle, false);
        player.animator.SetFloat(Settings.movementDirection, movementByVelocity.moveAngle);
    }

    private void SetMovementAnimationParameters(MovementToPositionArgs movementToPositionArgs)
    {
        if (movementToPositionArgs.isRolling)
        {
            player.animator.SetBool(Settings.isRolling, true);
        }
    }

        private void SetIdleAnimationParameters()
    {
        player.animator.SetBool(Settings.isMoving, false);
        player.animator.SetBool(Settings.isIdle, true);
    }

    private void InitalizeRollAnimationParameters()
    {
        player.animator.SetBool(Settings.isRolling, false);
    }
}