using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySettingBtn : MonoBehaviour
{
    public GameObject KeySettingPopup;

    public void ShowPopup()
    {
        Managers.UI.ShowPopupUI<UI_Popup>("KeySettingPopup");
    }
}
