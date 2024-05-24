using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BTablet : UI_Base
{
    public void Setting_Popup()
    {
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Setting");
    }

    public void PlayerStat_Popup()
    {
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_Popup>("Tablet/PlayerStat");
    }

    public void Inventory_Popup()
    {
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Inventory");
    }

    public void Collection_Popup()
    {
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Collection");
    }


    public override void Init()
    {

    }
}
