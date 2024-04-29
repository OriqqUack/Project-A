using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movemnet_Dummy : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1f;

    private float _inputTimer = Mathf.Infinity;
    private float _inputInterval = 0.3f;

    private void Update()
    {
        updateSequnce();
    }

    void updateSequnce()
    {
        _inputTimer += Time.deltaTime;

        PlayerMove();

        if(_inputTimer >= _inputInterval)
        {
            ControllSpeed();
            _inputTimer = 0f;
        }
    }

    private void PlayerMove()
    {
        float xValue = Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;
        float zValue = Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(xValue, 0, zValue));
    }

    private void ControllSpeed()
    {
        if (Input.GetKey(KeyCode.LeftBracket))
        {
            _moveSpeed -= 1f;
            Debug.Log("MoveSpeed :" + _moveSpeed);
        }
        else if (Input.GetKey(KeyCode.RightBracket))
        {
            _moveSpeed += 1f;
            Debug.Log("MoveSpeed :" + _moveSpeed);
        }      
    }

}
