using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NPCStatement;
using static QuestStatement;

public class NPCInteractionManager : MonoBehaviour //был синглтон
{
    public QuestStatement questStatement;
    public NPCStatement npcStatement;
    public DialogueManager dialogueManager;
    public QuestSystem questSystem;
    public QuestSO questSO;

    //создать список квестов.
    public List<QuestSO> quests = new List<QuestSO>();

    private void OnEnable()
    {
        Trigger.OnInteract += CheckQuestStatus;
    }
    private void OnDisable()
    {
        Trigger.OnInteract -= CheckQuestStatus;
    }
    void CheckQuestStatus()
    {
        if (quests.Count == 0)
            return;

        switch (questStatement.currentQuest)
        {
            case QuestState.QuestNotActive:
                dialogueManager.PlayDialogue();
                questStatement.currentQuest = QuestState.QuestActive;
                npcStatement.currentState = NPCState.QuestStart;
                questSystem.QuestStart();
                Debug.Log($"current questState: {questStatement.currentQuest}");
                break;
            case QuestState.QuestActive:
                if (questSystem.RequiredItemSearch())
                {
                    questStatement.currentQuest = QuestState.QuestComplete;
                    npcStatement.currentState = NPCState.QuestComplete;
                    Debug.Log($"current questState: {questStatement.currentQuest}");
                    dialogueManager.PlayDialogue();
                    return;
                }
                dialogueManager.PlayDialogue();
                Debug.Log($"current questState: {questStatement.currentQuest}");
                break;
            case QuestState.QuestComplete:
                int nextQuestIndex = quests.IndexOf(questSO) + 1;
                if (nextQuestIndex < quests.Count)
                {
                    questSO = quests[nextQuestIndex]; //nextQuestIndex это индекс квеста
                    questStatement.currentQuest = QuestState.QuestNotActive;
                }
                questSystem.QuestComplete();
                Debug.Log($"current questState: {questStatement.currentQuest}");
                break;
        }
    }
}
