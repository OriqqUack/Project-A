using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MMF_Player player;

    public void OnPointerEnter(PointerEventData eventData)
    {
        player?.PlayFeedbacks();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        player?.ResumeFeedbacks();
    }

    void OnDisable()
    {
        player?.ResumeFeedbacks();
    }
}
