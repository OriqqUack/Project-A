using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScript : MonoBehaviour
{
    public void Single()
    {
        Managers.Input._isSingle = true;
        Managers.Scene.LoadScene(Define.Scene.Game);
    }

    public void Multi()
    {
        Managers.Input._isSingle = false;
        Managers.Scene.LoadScene(Define.Scene.Start);
    }

    public void UI_Close()
    {
        Managers.UI.ClosePopupUI();
    }
}
