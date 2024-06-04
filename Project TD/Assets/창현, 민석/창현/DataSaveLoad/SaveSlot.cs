using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using MoreMountains.Feedbacks;

public class SaveSlot : UI_Base, IPointerEnterHandler, IPointerExitHandler
{
    private string slotName = "비어있음";
    private float playTime;

    public MMF_Player mmfPlayer;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        mmfPlayer?.PlayFeedbacks();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    void OnDisable()
    {
        mmfPlayer?.ResumeFeedbacks();
    }
}
