using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action KeyAction = null;
    

    //bool _pressed = false;
    //float _pressedTime = 0;

    

    public void OnUpdate()
    {
        if (Input.anyKey == false)
            return;

        if (KeyAction != null)
            KeyAction.Invoke();
    }

    public void Clear()
    {
        KeyAction = null;
    }
}

//if (EventSystem.current.IsPointerOverGameObject())
//    return;

//if (Input.anyKey && KeyAction != null)
//    KeyAction.Invoke();

//if (KeyAction != null)
//{
//    if (Input.GetKey(KeyCode.W))
//    {
//        if (!_pressed)
//        {
//            KeyAction.Invoke(Define.KeyEvent.Foward);
//            _pressedTime = Time.time;
//        }
//        KeyAction.Invoke(Define.MouseEvent.Press);
//        _pressed = true;
//    }
//    else
//    {
//        if (_pressed)
//        {
//            if (Time.time < _pressedTime + 0.2f)
//                KeyAction.Invoke(Define.MouseEvent.Click);
//            KeyAction.Invoke(Define.MouseEvent.PointerUp);
//        }
//        _pressed = false;
//        _pressedTime = 0;
//    }
//}