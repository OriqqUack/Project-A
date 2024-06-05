using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Collection : UI_Base
{
    public void Achievement_Popup()
    {
        Managers.UI.CloseAllPopupUI();
        Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Collection_Detail/Achievement");
    }

    public void Monster_Popup()
    {
        Managers.UI.CloseAllPopupUI();
        Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Collection_Detail/Monster");
    }

    public void Weapon_Popup()
    {
        Managers.UI.CloseAllPopupUI();
        Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Collection_Detail/Weapon");
    }

    public void Back()
    {
        Managers.UI.CloseAllPopupUI();
        Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Tablet");
        Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Collection");
    }


    public override void Init()
    {
        
    }
}
