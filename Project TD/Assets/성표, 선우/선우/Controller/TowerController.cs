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
            // 타워의 기능이 고장나 있는 상태
            return;
        }
    }

    public void DisableTower()
    {
        isDisabled = true;
        // 타워 고장 시 추가 로직이 필요하면 여기 추가
    }

    public void RepairTower()
    {
        isDisabled = false;
        // 타워 수리 시 추가 로직이 필요하면 여기 추가
    }
}
