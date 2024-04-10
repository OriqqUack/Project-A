using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager   // 상태 체크
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;
    public Action<Define.State> Key = null;

    public string lastKey;

    public void OnUpdate()
    {
        if (Input.anyKey == false) return;

        if (KeyAction != null)
        {
            KeyAction.Invoke();
        }
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
        Key = null;
    }
}
