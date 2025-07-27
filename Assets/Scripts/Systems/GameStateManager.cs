using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestStatement;
using static NPCStatement;

public class GameStateManager : Singleton<GameStateManager>
{
    //информация о количестве страниц дневников

    private Dictionary<string, QuestState> questStates = new();
    private NPCState npcState;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetProgress();
            Debug.Log("Прогресс сброшен!");
        }
    }
    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
    }
    public void RegisterQuestState(string id, QuestState state) 
    {
        questStates[id] = state;
        Debug.Log($"Квест {id} зарегистрирован со статусом {state}");
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
    public void MarkItemAsPicked(string id) 
    {
        PlayerPrefs.SetInt("item_" + id, 1);
        PlayerPrefs.Save();
    }
    public bool IsItemPicked(string id) 
    {
        return PlayerPrefs.GetInt("item_" + id, 0) == 1;
    }
    public void SetDoorState(string doorID, bool isOpen) 
    {
        PlayerPrefs.SetInt("door_" + doorID, isOpen ? 1 : 0);
        PlayerPrefs.Save();
    }
    public bool GetDoorState(string doorID) 
    {
        return PlayerPrefs.GetInt("door_" + doorID, 0) == 1;
    }
    public void MarkObjectAsInteracted(GameObject objectName, bool isActive) 
    {
        PlayerPrefs.SetInt("object_" + objectName, isActive? 1 : 0);
        PlayerPrefs.Save();
    }
    public bool IsObjectMarked(GameObject objectName) 
    {
        return PlayerPrefs.GetInt("object_" + objectName, 0) == 1;
    }
}
