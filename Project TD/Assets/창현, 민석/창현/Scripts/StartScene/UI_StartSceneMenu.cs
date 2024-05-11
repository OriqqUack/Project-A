using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class UI_StartSceneMenu : UI_Base
{
    enum GameObjects 
    {
        Start,
        Setting,
        Credit,
        Quit
    }

    public CinemachineVirtualCamera menuCam;
    public CinemachineVirtualCamera saveCam;
    public CinemachineVirtualCamera settingCam;

    public override void Init()
    {
        Managers.UI.ShowUI("Buttons");
        Bind<Button>(typeof(GameObjects));
        GetButton((int)GameObjects.Start).onClick.AddListener(delegate { OnClickedStartBtn(); });
        GetButton((int)GameObjects.Setting).onClick.AddListener(delegate { OnClickedSettingBtn(); });
    }

    public void OnClickedStartBtn()
    {
        Managers.UI.CloseUI();
        //this.transform.GetChild(0).gameObject.SetActive(false);
        saveCam.MoveToTopOfPrioritySubqueue();
    }

    public void OnClickedSettingBtn()
    {
        Managers.UI.CloseUI();
        settingCam.MoveToTopOfPrioritySubqueue();
    }

    public void OnClickedCreditBtn()
    {

    }

    public void OnClickedQuitBtn()
    {

    }

    public void OnClickedCancelBtn()
    {
        menuCam.MoveToTopOfPrioritySubqueue();
        Managers.UI.ShowUI("Buttons");
        Debug.Log("ClickedCancel");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Managers.UI.PopupUIStack.Count == 0)
            {
                menuCam.MoveToTopOfPrioritySubqueue();
                Managers.UI.ShowUI("Buttons");
            }
            Debug.Log("EscClicked");
        }
    }

}
