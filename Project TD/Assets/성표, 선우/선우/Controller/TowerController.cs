using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public bool isDisabled = false;
    
    void Update()
    {
        if (isDisabled)
        {
            // Ÿ���� ����� ���峪 �ִ� ����
            return;
        }
    }

    public void DisableTower()
    {
        isDisabled = true;
        // Ÿ�� ���� �� �߰� ������ �ʿ��ϸ� ���� �߰�
    }

    public void RepairTower()
    {
        isDisabled = false;
        // Ÿ�� ���� �� �߰� ������ �ʿ��ϸ� ���� �߰�
    }
}
