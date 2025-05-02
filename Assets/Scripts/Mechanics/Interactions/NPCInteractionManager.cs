using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NPCStatement;
using static QuestStatement;

public class NPCInteractionManager : MonoBehaviour
{
    public QuestStatement questStatement;
    public NPCStatement npcStatement;
    public DialogueManager dialogueManager;
    public QuestSystem questSystem;
    public QuestSO questSO;

    public List<QuestSO> quests = new List<QuestSO>();

    private void Start()
    {
        if (quests == null) 
            Debug.Log("quests null");
        if(questSO == null)
            Debug.Log("Quest null");

        questStatement.currentQuest = GameStateManager.Instance.GetQuestState(questSO.name);
        Debug.Log($"Квест {questSO.name} зарегистрирован со статусом {questStatement.currentQuest}");
        npcStatement.currentState = GameStateManager.Instance.GetNPCState();
        Debug.Log($"NPC зарегистрирован со статусом {npcStatement.currentState}");
    }

    private void OnEnable() => InteractionController.OnInteract += CheckQuestStatus;
    private void OnDisable() => InteractionController.OnInteract -= CheckQuestStatus;

    private void QuestStateChanged() 
    {
        GameStateManager.Instance.RegisterQuestState(questSO.name, questStatement.currentQuest);
    }
    private void NPCStateChanged() 
    {
        GameStateManager.Instance.RegisterNPCState(npcStatement.currentState);
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
                QuestStateChanged();
                npcStatement.currentState = NPCState.QuestStart;
                NPCStateChanged();
                Debug.Log($"current questState: {questStatement.currentQuest}");
                break;
            case QuestState.QuestActive:
                if (questSystem.RequiredItemSearch())
                {
                    questStatement.currentQuest = QuestState.QuestComplete;
                    QuestStateChanged();
                    npcStatement.currentState = NPCState.QuestComplete; 
                    NPCStateChanged();
                    Debug.Log($"current questState: {questStatement.currentQuest}");
                    dialogueManager.PlayDialogue();
                    return;
                }
                dialogueManager.PlayDialogue();
                Debug.Log($"current questState: {questStatement.currentQuest}");
                break;
            //case QuestState.QuestComplete:
            //    Debug.Log("проверка 1");
            //    int nextQuestIndex = quests.IndexOf(questSO) + 1;
            //    Debug.Log("проверка 2");
            //    if (nextQuestIndex < quests.Count)
            //    {
            //        Debug.Log("проверка if 3");
            //        questSO = quests[nextQuestIndex]; //nextQuestIndex это индекс квеста
            //        Debug.Log("проверка if 4");
            //        questStatement.currentQuest = QuestState.QuestNotActive;
            //        Debug.Log("проверка if 5");
            //        QuestStateChanged();
            //        Debug.Log("проверка if 6");
            //        npcStatement.currentState = NPCState.DefaultState;
            //        Debug.Log("проверка if 7");
            //        NPCStateChanged();
            //        Debug.Log("проверка if 8");
            //        Debug.Log($"current questState: {questStatement.currentQuest}");
            //        Debug.Log("проверка if 9");
            //        return;
            //    }
            //    else 
            //    {
            //        Debug.Log("проверка else 1");
            //        questStatement.currentQuest = QuestState.AllQuestsDone;
            //        Debug.Log("проверка else 2");
            //        QuestStateChanged();
            //        Debug.Log("проверка else 3");
            //        npcStatement.currentState = NPCState.DefaultState;
            //        Debug.Log("проверка else 4");
            //        NPCStateChanged();
            //        Debug.Log("проверка else 5");
            //        Debug.Log($"current questState: {questStatement.currentQuest}");
            //    }
            //break;
            case QuestState.QuestComplete:
                questStatement.currentQuest = QuestState.AllQuestsDone;
                QuestStateChanged();
                npcStatement.currentState = NPCState.DefaultState;
                NPCStateChanged();
                dialogueManager.PlayDialogue();
                Debug.Log($"current questState: {questStatement.currentQuest}");
                break;
            case QuestState.AllQuestsDone:
                Debug.Log("Все квесты завершены — обычный диалог.");
                dialogueManager.PlayDialogue();
                break;
        }
    }
}
