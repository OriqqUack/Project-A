using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    #region Tooltip
    [Tooltip("Movement Detail Scriptavel object")]
    #endregion Tooltip
    [SerializeField] private MovementDetailsSO movementDetails;

    private Player player;
    private float moveSpeed;
    private Coroutine playerRollCoroutine;
    private WaitForFixedUpdate waitForFixedUpdate;
    private bool isPlayerRolling = false;
    private float playerRollCooldownTimer = 0f;

    private void Awake()
    {
        // load component
        player = GetComponent<Player>();
    }

    private void Start()
    {
        // 코루틴에 사용하는 WaitForFixedUdate 생성
        waitForFixedUpdate = new WaitForFixedUpdate();
    }


}
