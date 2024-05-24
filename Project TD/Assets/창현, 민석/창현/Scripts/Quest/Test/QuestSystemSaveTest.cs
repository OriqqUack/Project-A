using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystemSaveTest : MonoBehaviour
{
    [SerializeField]
    private Quest quest;
    [SerializeField]
    private Category category;
    [SerializeField]
    private TaskTarget target;

    private void Start()
    {
        var questSystem = Managers.Quest;

        if (questSystem.ActiveQuests.Count == 0)
        {
            Debug.Log("Register");
            var newQuest = questSystem.Register(quest);
        }
        else
        {
            questSystem.onQuestCompleted += (quest) =>
            {
                Debug.Log("Complete");
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
            };
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Á¾·á");
        Managers.Quest.Save();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Managers.Quest.ReceiveReport(category, target, 1);
    }
}
