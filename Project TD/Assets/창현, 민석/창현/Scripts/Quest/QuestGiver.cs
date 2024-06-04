using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//예제 시작 시 Quest를 등록하는 스크립트
public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private Quest[] quests;

    private void Start()
    {
        foreach (var quest in quests)
        {
            if (quest.IsAcceptable && !QuestSystem.Instance.ContainsInCompletedQuests(quest))
                QuestSystem.Instance.Register(quest);
        }
    }

}
