using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat_Camera : MonoBehaviour
{
    [SerializeField]
    private Transform _playerBody;
    [SerializeField]
    private float distance = 4.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = _playerBody.position;

        // �÷��̾��� ������ ������
        Vector3 playerDirection = _playerBody.forward;

        // �÷��̾��� ������ ����Ͽ� ī�޶��� ��ġ�� ���
        Vector3 cameraPosition = targetPosition + playerDirection * distance;

        // ���� ��ġ�� ī�޶� ��ġ�� ����
        transform.position = cameraPosition;

        // ī�޶� �÷��̾ �ٶ󺸵��� ȸ��
        transform.LookAt(targetPosition);
    }
}
