using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserIcon_Camera : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    private void Update()
    {
        transform.position = _player.transform.position + new Vector3(0, 2.8f, 1.8f);
        float angle = _player.transform.rotation.eulerAngles.y - 180f;
        transform.rotation = Quaternion.Euler(_player.transform.rotation.x, angle, _player.transform.rotation.z);
    }
}
