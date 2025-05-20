using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestStatement;
using static NPCStatement;

public class GameStateManager : Singleton<GameStateManager>
{
    //TODO: ����� ������ � ���� ������
    //���������� � ���������� ������� ���������

    private Dictionary<string, QuestState> questStates = new();
    private NPCState npcState;
    public void RegisterQuestState(string id, QuestState state) 
    {
        questStates[id] = state;
        Debug.Log($"����� {id} ��������������� �� �������� {state}");
    }
    public QuestState GetQuestState(string id) 
    {
        return questStates.TryGetValue(id, out var state) ? state : QuestState.QuestNotActive;
    }
    public void RegisterNPCState(NPCState state) 
    {
        npcState = state;
    }
    public NPCState GetNPCState() 
    {
        return npcState;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetProgress();
            Debug.Log("�������� �������!");
        }
    }
    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll(); 
    }
    public void MarkItemAsPicked(string id) 
    {
        PlayerPrefs.SetInt("item_" + id, 1);
        PlayerPrefs.Save();
    }
    public bool IsItemPicked(string id) 
    {
        return PlayerPrefs.GetInt("item_" + id, 0) == 1;
    }
}
