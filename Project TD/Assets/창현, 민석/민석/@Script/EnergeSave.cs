using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergeSave : MonoBehaviour
{
    PlayerStat _stat;

    private void Start()
    {
        GameObject _player = Managers.Game.GetPlayer();
        _stat = _player.GetComponent<PlayerStat>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Define._hasEnerge == false)
            return;

        if (other.CompareTag("Player") && Define._hasEnerge == true)
        {
            _stat.Energe += 100;
            Define._hasEnerge = false;
        }
    }

    void Update()
    {
        
    }
}
