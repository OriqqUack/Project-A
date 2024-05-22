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

        GetButton((int)GameObjects.Confirm).onClick.AddListener(delegate { });
        GetButton((int)GameObjects.Cancel).onClick.AddListener(delegate { });
        GetButton((int)GameObjects.Button_Close).onClick.AddListener(delegate { });
        GetButton((int)GameObjects.Save).onClick.AddListener(delegate { });
        GetButton((int)GameObjects.PopupCancel).onClick.AddListener(delegate { });
    }

    public void OnClickedConfirmBtn()
    {
        KeyManager.instance.SaveOptionData();
    }

    public void OnClickedCancelBtn()
    {
        KeyManager.instance.CancelOptionData();
        Managers.UI.ClosePopupUI();
    }

    public void OnClickedButtonClose()
    {
        if (!KeyManager.instance.isKeyChanged)
        {
            Managers.UI.ClosePopupUI();
            return;
        }

        KeyManager.instance.CancelOptionData();
        Managers.UI.ClosePopupUI();
    }

    public void KeyEvent()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (KeyManager.instance.isKeyChanged)
            {
                confirmPopup.SetActive(true);
            }
        }
    }
}
