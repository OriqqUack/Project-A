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

        // �÷��̾��� ������ ������
        Vector3 playerDirection = _playerHead.forward;

        // �÷��̾��� ������ ����Ͽ� ī�޶��� ��ġ�� ���
        Vector3 cameraPosition = targetPosition + playerDirection * distance;

        // ���� ��ġ�� ī�޶� ��ġ�� ����
        transform.position = cameraPosition;

        // ī�޶� �÷��̾ �ٶ󺸵��� ȸ��
        transform.LookAt(targetPosition);
    }
}
