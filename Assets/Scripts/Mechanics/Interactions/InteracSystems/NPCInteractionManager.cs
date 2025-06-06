using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NPCStatement;
using static QuestStatement;

public class NPCInteractionManager : MonoBehaviour, IInteractable
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
        npcStatement.currentState = GameStateManager.Instance.GetNPCState();
    }
    private void QuestStateChanged() => GameStateManager.Instance.RegisterQuestState(questSO.name, questStatement.currentQuest);
    private void NPCStateChanged() => GameStateManager.Instance.RegisterNPCState(npcStatement.currentState);
    public void Interact()
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
                if (questSystem.RequiredItemsSearch())
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
