using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NPCStatement;
using static QuestStatement;

public class NPCInteractionManager : MonoBehaviour, IInteractable
{
    [SerializeField] private QuestStatement questStatement;
    [SerializeField] private NPCStatement npcStatement;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private QuestSystem questSystem;
    [SerializeField] private QuestSO questSO;

    [SerializeField] private List<QuestSO> quests = new List<QuestSO>();

    public static event Action OnQuestEnable;
    public static event Action OnQuestComplete;

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
                OnQuestEnable.Invoke();
                Debug.Log($"current questState: {questStatement.currentQuest}");
                UIManager.Instance.QuestUI.ActivateUI();
                break;
            case QuestState.QuestActive:
                if (questSystem.RequiredItemsSearch())
                {
                    questStatement.currentQuest = QuestState.QuestComplete;
                    QuestStateChanged();
                    npcStatement.currentState = NPCState.QuestComplete; 
                    NPCStateChanged();
                    Debug.Log($"current questState: {questStatement.currentQuest}");
                    OnQuestComplete.Invoke();
                    return;
                }
                dialogueManager.PlayDialogue();
                Debug.Log($"current questState: {questStatement.currentQuest}");
                break;
        }
    }
}
