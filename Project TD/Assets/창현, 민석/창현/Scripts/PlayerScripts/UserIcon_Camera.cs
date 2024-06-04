using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserIcon_Camera : MonoBehaviour
{
    [SerializeField]
    private Transform _playerHead;
    [SerializeField]
    private float distance = 3.0f;

    private void Update()
    {
        Vector3 targetPosition = _playerHead.position;

        // 플레이어의 방향을 가져옴
        Vector3 playerDirection = _playerHead.forward;

        // 플레이어의 방향을 고려하여 카메라의 위치를 계산
        Vector3 cameraPosition = targetPosition + playerDirection * distance;

        // 계산된 위치로 카메라 위치를 조정
        transform.position = cameraPosition;

        // 카메라가 플레이어를 바라보도록 회전
        transform.LookAt(targetPosition);
    }
}
