using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnergeController : MonoBehaviour
{
    public static float _timer = 0.0f;
    private bool _approch = false;

    void Update()
    {
        if (_approch)
            _timer += Time.deltaTime;

        if (_timer > 5.0f)
        {
            _timer = 0.0f;
            _approch = false;
            Define._hasEnerge = true;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Define._hasEnerge == true)
            return;

        if (other.CompareTag("Player"))
            _approch = true;
    }

    // 플레이어 움직일 때 취소
    private void OnTriggerExit(Collider other)
    {
        _approch = false;
        _timer = 0.0f;
    }
}
