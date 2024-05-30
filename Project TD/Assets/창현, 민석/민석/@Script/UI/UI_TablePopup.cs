using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito;
public class UI_TablePopup : UI_Base
{

    public void Setting()
    {
        Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Tablet");
        Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Setting");

    }

    public void PlayerStat()
    {
        Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Tablet");
        Managers.UI.ShowPopupUI<UI_Popup>("Tablet/PlayerStat");

    }

    public void Inventory()
    {
        Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Tablet");
        Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Inventory");

    }


    public override void Init()
    {
        
    }
}
