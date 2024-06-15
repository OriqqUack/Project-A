using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeySettingPopup : UI_Base
{
    [SerializeField]
    private GameObject confirmPopup;

    enum GameObjects
    {
        Confirm,
        Cancel,
        Button_Close,
        Save,
        PopupCancel
    }

    public override void Init()
    {
        Bind<Button>(typeof(GameObjects));

        GetButton((int)GameObjects.Confirm).onClick.AddListener(delegate { OnClickedConfirmBtn(); });
        GetButton((int)GameObjects.Cancel).onClick.AddListener(delegate { OnClickedCancelBtn(); });
        GetButton((int)GameObjects.Button_Close).onClick.AddListener(delegate { OnClickedButtonClose(); });
        GetButton((int)GameObjects.Save).onClick.AddListener(delegate { OnClickedSaveBtn(); });
        GetButton((int)GameObjects.PopupCancel).onClick.AddListener(delegate { OnClickedPopupCancel(); });


        Managers.Input.KeyAction -= KeyEvent;
        Managers.Input.KeyAction += KeyEvent;

        confirmPopup.SetActive(false);
    }

    public void OnClickedConfirmBtn()
    {
        KeyManager.Instance.SaveOptionData();
        Managers.UI.ClosePopupUI();
    }

    public void OnClickedCancelBtn()
    {
        KeyManager.Instance.LoadOptionData();
        Managers.UI.ClosePopupUI();
    }

    public void OnClickedButtonClose()
    {
        if (!KeyManager.Instance.isKeyChanged)
        {
            Managers.UI.ClosePopupUI();
            return;
        }

        confirmPopup.SetActive(true);
    }

    public void OnClickedSaveBtn()
    {
        KeyManager.Instance.SaveOptionData();
        Managers.UI.ClosePopupUI();
    }
    
    public void OnClickedPopupCancel()
    {
        KeyManager.Instance.LoadOptionData();
        Managers.UI.ClosePopupUI();
    }

    public void KeyEvent()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (KeyManager.Instance.isKeyChanged)
            {
                confirmPopup.SetActive(true);
            }
            else
            {
                Managers.UI.ClosePopupUI();
            }
        }
    }
}
