using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using URPGlitch.Runtime.AnalogGlitch;


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

    [SerializeField]
    private Volume volume;

    private AnalogGlitchVolume analog;

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
        StartCoroutine(TriggerGlitch(false));
    }

    public void OnClickedSettingBtn()
    {
        Managers.UI.CloseUI();
        settingCam.MoveToTopOfPrioritySubqueue();
        StartCoroutine(TriggerGlitch(false));
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
        StartCoroutine(TriggerGlitch(true));
        Debug.Log("ClickedCancel");
    }

    IEnumerator TriggerGlitch(bool isCancelBtn)
    {
        if (volume.profile.TryGet(out analog))
        {
            analog.active = true;
        }
        yield return new WaitForSeconds(1.3f);

        if(isCancelBtn)
            Managers.UI.ShowUI("Buttons");
        analog.active = false;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Managers.UI.PopupUIStack.Count == 0)
            {
                menuCam.MoveToTopOfPrioritySubqueue();
                StartCoroutine(TriggerGlitch(true));
            }
            Debug.Log("EscClicked");
        }
    }

}
