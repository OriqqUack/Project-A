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

    private int _order;

    public override void Init()
    {
        Bind<Button>(typeof(GameObjects));
        GetButton((int)GameObjects.Start).onClick.AddListener(delegate { OnClickedStartBtn(); });
        GetButton((int)GameObjects.Setting).onClick.AddListener(delegate { OnClickedSettingBtn(); });
    }

    public void OnClickedStartBtn()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        saveCam.MoveToTopOfPrioritySubqueue();
        _order++;
    }

    public void OnClickedSettingBtn()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        settingCam.MoveToTopOfPrioritySubqueue();
        _order++;
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
        Debug.Log("ClickedCancel");

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_order != 0)
            {
                menuCam.MoveToTopOfPrioritySubqueue();
                this.transform.GetChild(0).gameObject.SetActive(true);
            }
            Debug.Log("EscClicked");
        }
    }

}
