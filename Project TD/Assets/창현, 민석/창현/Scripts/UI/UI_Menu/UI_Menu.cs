using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Menu : UI_Base
{
    GameObject activeInfoUI;

    int currentOrder;
    enum HotBarUI
    {
        PlayerStat,
        Inventory,
        Craft,
        Npc,
        Index,
        Setting
    }

    enum UI 
    { 
        MenuName
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(HotBarUI));
        Bind<TextMeshProUGUI>(typeof(UI));

        foreach (HotBarUI go in System.Enum.GetValues(typeof(HotBarUI)))
        {
            GetObject((int)go).SetActive(false);
        }
    }

    public void OnClickMenuEvent(GameObject gameObject)
    {
        if (activeInfoUI != null)
        {
            activeInfoUI.SetActive(false);

            if (activeInfoUI == gameObject)
                activeInfoUI = null;
            else
            {
                activeInfoUI = gameObject;
                activeInfoUI.SetActive(true);
            }
        }
        else
        {
            activeInfoUI = gameObject;
            activeInfoUI.SetActive(true);
        }

        GetText((int)UI.MenuName).text = gameObject.name;
    }

    public void OnClickLeftArrowEvent()
    {
        if (activeInfoUI == null)
            return;

        activeInfoUI.SetActive(false);
        currentOrder--;
        activeInfoUI = GetObject(currentOrder);
        GetText((int)UI.MenuName).text = GetObject(currentOrder).name;
        activeInfoUI.SetActive(true);
    }

    public void OnClickRightArrowEvent()
    {
        if (activeInfoUI == null)
            return;

        activeInfoUI.SetActive(false);
        currentOrder++;
        activeInfoUI = GetObject(currentOrder);
        GetText((int)UI.MenuName).text = GetObject(currentOrder).name;
        activeInfoUI.SetActive(true);
    }

    private void Update()
    {
        if (Managers.Input.lastKey == "B")
        {
            
            if (activeInfoUI != null)
                activeInfoUI.SetActive(false);

            activeInfoUI = GetObject((int)HotBarUI.PlayerStat);
            activeInfoUI.SetActive(true);
            Managers.Input.lastKey = null;

            GetText((int)UI.MenuName).text = GetObject((int)HotBarUI.PlayerStat).name;
            currentOrder = 0;
        }
        if(Managers.Input.lastKey == "I")
        {
            if (activeInfoUI != null)
                activeInfoUI.SetActive(false);

            activeInfoUI = GetObject((int)HotBarUI.Inventory);
            activeInfoUI.SetActive(true);
            Managers.Input.lastKey = null;

            GetText((int)UI.MenuName).text = GetObject((int)HotBarUI.Inventory).name;
            currentOrder = 1;
        }
    }
}
