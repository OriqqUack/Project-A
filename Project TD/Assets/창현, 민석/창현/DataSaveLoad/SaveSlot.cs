using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveSlot : UI_Base
{
    private string slotName = "비어있음";
    private float playTime;

    public string SlotName 
    {
        get 
        {
            return slotName; 
        }
        set
        {
            slotName = value;
            GetText((int)GameObjects.Text_Title).text = $"{value}";
        }
    }
    public float PlayTime 
    { 
        get 
        {
            return playTime; 
        }
        set
        {
            playTime = value;
            GetText((int)GameObjects.PlayTime).text = $"{value}";
        }
    }

    enum GameObjects 
    {
        Text_Title,
        PlayTime
    }

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(GameObjects));
    }

    private void Start()
    {
    }
}
