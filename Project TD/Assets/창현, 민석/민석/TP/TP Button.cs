using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPButton : MonoBehaviour
{
    public void GOTOpoint0()
    {
        Managers.UI.ClosePopupUI();
        Managers.Input._tp0 = true;
    }

    public void GOTOpoint1()
    {
        Managers.UI.ClosePopupUI();
        Managers.Input._tp1 = true;
    }

    public void GOTOpoint2()
    {
        Managers.UI.ClosePopupUI();
        Managers.Input._tp2 = true;
    }

    public void Back()
    {
        Managers.UI.ClosePopupUI();
    }
}
