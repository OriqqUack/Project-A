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
        Bind<Button>(typeof(GameObjects));

        menuCam = GameObject.Find("MenuCam").GetComponent<CinemachineVirtualCamera>();
        saveCam = GameObject.Find("SaveCam").GetComponent<CinemachineVirtualCamera>();
        settingCam = GameObject.Find("SettingCam").GetComponent<CinemachineVirtualCamera>();

        volume = GameObject.Find("ETC").transform.Find("PostProcessingGlitch").GetComponent<Volume>();

        GetButton((int)GameObjects.Start).onClick.AddListener(delegate { OnClickedStartBtn(); });
        GetButton((int)GameObjects.Setting).onClick.AddListener(delegate { OnClickedSettingBtn(); });
        GetButton((int)GameObjects.Quit).onClick.AddListener(delegate { OnClickedQuitBtn(); });

    }

    public void OnClickedStartBtn()
    {
        Managers.UI.ClosePopupUI();
        //this.transform.GetChild(0).gameObject.SetActive(false);
        saveCam.MoveToTopOfPrioritySubqueue();
        StartCoroutine(TriggerGlitch(false));
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void OnClickedSettingBtn()
    {
        Managers.UI.ClosePopupUI();
        settingCam.MoveToTopOfPrioritySubqueue();
        StartCoroutine(TriggerGlitch(false));
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void OnClickedCreditBtn()
    {
        
    }

    public void OnClickedQuitBtn()
    {
        Managers.UI.ShowPopupUI<UI_QuitPopup>("QuitPopup");
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
            gameObject.GetComponent<Canvas>().enabled = true;
        analog.active = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Managers.UI.PopupStack.Count == 0)
            {
                menuCam.MoveToTopOfPrioritySubqueue();
                StartCoroutine(TriggerGlitch(true));
            }
            Debug.Log("EscClicked");
        }
    }
}
