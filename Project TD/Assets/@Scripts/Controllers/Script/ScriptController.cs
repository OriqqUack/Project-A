using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScriptController : MonoBehaviour
{
    public void UI_Buy()
    {
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_Button>("Script/buyUI");
    }

    public void UI_Sell()
    {
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_Button>("Script/sellUI");
    }

    public void UI_Exit()
    {
        Managers.UI.ClosePopupUI();
    }
}
