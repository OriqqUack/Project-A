using Rito.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BTablet : UI_Base
{
    public void Setting_Popup()
    {
        if (Define._InvenActive)
        {
            Define._InvenActive = false;
            Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Setting");
        }
        else
        {
            Managers.UI.ClosePopupUI();
            Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Setting");
        }
        //Managers.UI.ClosePopupUI();
        //Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Setting");
    }

    public void PlayerStat_Popup()
    {
        if (Define._InvenActive)
        {
            Define._InvenActive = false;
            Managers.UI.ShowPopupUI<UI_Popup>("Tablet/PlayerStat");
        }
        else
        {
            Managers.UI.ClosePopupUI();
            Managers.UI.ShowPopupUI<UI_Popup>("Tablet/PlayerStat");
        }
    }

    public void Inventory_Popup()
    {
        if (Define._InvenActive)
        {
            Define._InvenActive = false;
            Managers.UI.ShowPopupUI<UI_Popup>("Inven");
        }
        else
        {
            Managers.UI.ClosePopupUI();
            Managers.UI.ShowPopupUI<UI_Popup>("Inven");
        }
        /*Managers.UI.ClosePopupUI();
        Define._InvenActive = true;
        Managers.UI.ShowPopupUI<UI_Popup>("Inven");*/
    }

    public void Collection_Popup()
    {
        if (Define._InvenActive)
        {
            Define._InvenActive = false;
            Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Collection");
        }
        else
        {
            Managers.UI.ClosePopupUI();
            Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Collection");
        }
        //Managers.UI.ClosePopupUI();
        //Managers.UI.ShowPopupUI<UI_Popup>("Tablet/Collection");
    }

    public override void Init()
    {

    }
}
