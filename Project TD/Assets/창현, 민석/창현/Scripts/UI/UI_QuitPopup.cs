using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuitPopup : UI_Popup
{
    enum GameObjects
    {
        Cancel,
        Quit
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(GameObjects));

        GetButton((int)GameObjects.Cancel).onClick.AddListener(delegate { OnClickedCancelBtn(); });
        GetButton((int)GameObjects.Quit).onClick.AddListener(delegate { OnClickedQuitBtn(); });
        Debug.Log(GetButton((int)GameObjects.Cancel).name);
    }

    public void OnClickedCancelBtn()
    {
        Managers.UI.ClosePopupUI();
    } 

    public void OnClickedQuitBtn()
    {
        Application.Quit();
    }
}