using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private GameObject questTextContainer;
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private QuestSO currentQuest;
    [SerializeField] private GameObject frameImage;

    private Dictionary<string, int> currentCounts = new();
    private void Awake()
    {
        questTextContainer.SetActive(false);
        frameImage.SetActive(false);
    }
    private void Start()
    {
        Inventory.ItemAdded += OnItemAdded;
    }
    public void ActivateUI() 
    {
        frameImage.SetActive(true);
        questTextContainer.SetActive(true);
        ShowRequiredItems();
        UpdateUI();
    }
    public void HideUI() 
    {
        questTextContainer.SetActive(false);
        frameImage.SetActive(false);
    }
    private void OnDestroy()
    {
        Inventory.ItemAdded -= OnItemAdded;
    }
    private void ShowRequiredItems() 
    {
        foreach (var req in currentQuest.requirements)
        {
            currentCounts[req.itemID] = 0;
        }
    }
    private void OnItemAdded(QuestItemSO item) 
    {
        string id = item.itemID;
        if (!currentCounts.ContainsKey(id)) return;

        currentCounts[id]++;
        UpdateUI();
    }
    private void UpdateUI() 
    {
        string text = $"Цель: {currentQuest.questName}\n";

        foreach (var req in currentQuest.requirements)
        {
            int current = currentCounts[req.itemID];
            int max = req.requiredStackSize;
            string name = req.item.itemName;
            text += $"{name} : {current}/{max}\n";
        }
        questText.text = text;  
    }
    public void ResetQuestState()
    {
        foreach (var req in currentQuest.requirements)
        {
            currentCounts[req.itemID] = 0;
        }
        questTextContainer.SetActive(false);
        frameImage.SetActive(false);
    }
}
