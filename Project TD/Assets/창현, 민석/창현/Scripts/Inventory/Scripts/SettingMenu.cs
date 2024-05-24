using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    GameObject activeUI;
    public void SettingBarControl(GameObject clickedObject)
    {
        if (activeUI != null)
        {
            activeUI.SetActive(false);

            if (activeUI == clickedObject)
                activeUI = null;
            else
            {
                activeUI = clickedObject;
                activeUI.SetActive(true);
            }
        }
        else
        {
            activeUI = clickedObject;
            activeUI.SetActive(true);
        }
    }

    public void Save() 
    {
        DataController.instance.SaveData();
    }
}
