using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ���� �� Quest�� ����ϴ� ��ũ��Ʈ
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
