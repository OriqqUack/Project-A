using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_UI_GetEXP : MonoBehaviour
{
    PlayerStat _stat;

    private void Start()
    {
        _stat = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
    }

    public void OnClicked()
    {
        _stat.Exp += 5;
    }
}
