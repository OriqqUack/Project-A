using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveSlotDeleteBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MMF_Player mmfPlayer;

    public void OnPointerEnter(PointerEventData eventData)
    {
        mmfPlayer?.PlayFeedbacks();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mmfPlayer?.ResumeFeedbacks();
    }
    void OnDisable()
    {
        mmfPlayer?.ResumeFeedbacks();
    }
}
