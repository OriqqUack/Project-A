using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private MovementDetailsSO movementDetails;

    private Player player;
    private float moveSpeed;
    private Coroutine playerRollCoroutine;
    private WaitForFixedUpdate waitForFixedUpdate;
    private bool isPlayerRolling = false;
    private float playerRollCooldownTimer = 0f;

    private LayerMask groundLayerMask;

    private void Awake()
    {
        // load component
        player = GetComponent<Player>();

        moveSpeed = movementDetails.MoveSpeed;
        groundLayerMask = LayerMask.GetMask("BaseRaycastLayer");
    }

    private void Start()
    {
        // 코루틴을 사용할때 사용하는 waitforfixedUpdate 생성
        waitForFixedUpdate = new WaitForFixedUpdate();

        SetPlayerAnimationSpeed();
    }

    private void SetPlayerAnimationSpeed()
    {
        // 이동속도와 관련해 플레이어 애니메이션 재생 속도 설정
        player.animator.speed = moveSpeed / Settings.baseSpeedForPlayerAnimatons;
    }

    private void Update()
    {
        if (isPlayerRolling) return;

        RotatePlayerToAim();

        // 이동 관련 입력 처리 함수
        MovementInput();

        // 대쉬 쿨타임 함수
        PlayerRollCooldownTimer();
    }

    // 이동 관련 입력 처리 함수
    private void MovementInput()
    {
        // get movement input
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        bool spaceBarButtonDown = Input.GetKeyDown(KeyCode.Space);

        // create a direction vector based on the input
        Vector3 direction = new Vector3(horizontalMovement, 0 , verticalMovement);

        if (horizontalMovement != 0f && verticalMovement != 0f)
        {
            //direction *= 0.7f;
            direction = direction.normalized;
        }

        // if there is movement either move or roll
        if (direction != Vector3.zero)
        {
            if (!spaceBarButtonDown)
            {
                // trigger movement event
                player.movementByVelocityEvent.CallMovementByVElocityEvent(direction, moveSpeed);
            }
            // else player roll if not cooling down
            else if (playerRollCooldownTimer <= 0f)
            {
                PlayerRoll(direction);
            }
        }
        // else trigger idle event
        else
        {
            player.idleEvent.CallIdleEvent();
        }
    }

    /// <summary>
    /// 대쉬
    /// </summary>
    /// <param name="direction"></param>
    private void PlayerRoll(Vector3 direction)
    {
        playerRollCoroutine = StartCoroutine(PlayerRollRoutine(direction));
    }

    /// <summary>
    /// 플레이어 대쉬 코루틴
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private IEnumerator PlayerRollRoutine(Vector3 direction)
    {
        // minDistance used to decide when to exit corroutine loop
        float minDistance = 0.2f;
        isPlayerRolling = true;

        Vector3 targetPostion = player.transform.position + direction * movementDetails.rollDistance;

        while (Vector3.Distance(player.transform.position, targetPostion) > minDistance)
        {
            player.movementToPositionEvent.CallMovementToPostionEvent(targetPostion, player.transform.position, movementDetails.rollSpeed,
                direction, isPlayerRolling);

            yield return waitForFixedUpdate;
        }

        isPlayerRolling = false;

        playerRollCooldownTimer = movementDetails.rollCooldownTime;

        player.transform.position = targetPostion;
    }

    private void PlayerRollCooldownTimer()
    {
        if (playerRollCooldownTimer >= 0f)
        {
            playerRollCooldownTimer -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 대쉬 동작 시 플레이어와 사물의 충돌 처리
        StopPlayerRollRoutine();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 대쉬 동작 시 플레이어와 사물의 충돌 처리
        StopPlayerRollRoutine();
    }

    /// <summary>
    /// 대쉬 동작 정지 함수
    /// </summary>
    private void StopPlayerRollRoutine()
    {
        if (playerRollCoroutine != null)
        {
            StopCoroutine(playerRollCoroutine);

            isPlayerRolling = false;
        }
    }

    private void RotatePlayerToAim()
    {
        Vector3 targetPosition = HelperUtilitie.GetMouseWorldPosition(groundLayerMask);
        Debug.Log(targetPosition);
        targetPosition.y = gameObject.transform.position.y;

        Vector3 direction = targetPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
    }

    #region Validation
#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilitie.ValidateCheckNullValue(this, nameof(movementDetails), movementDetails);
    }

#endif
    #endregion Validation
}


