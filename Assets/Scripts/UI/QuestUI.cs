using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private GameObject questTextContainer;
    [SerializeField] private TextMeshProUGUI questText;
    public QuestSO currentQuest;

    private Dictionary<QuestItemSO, int> currentCounts = new();
    private void Awake()
    {
        questTextContainer.SetActive(false);
    }
    private void Start()
    {
        Inventory.ItemAdded += OnItemAdded;
    }
    public void ActivateUI() 
    {
        questTextContainer.SetActive(true);
        ShowRequiredItems();
        UpdateUI();
    }
    public void HideUI() 
    {
        questTextContainer.SetActive(false);
    }
    private void OnDestroy()
    {
        Inventory.ItemAdded -= OnItemAdded;
    }
    private void ShowRequiredItems() 
    {
        foreach (var req in currentQuest.requirements)
        {
            currentCounts[req.item] = 0;
        }
    }
    private void OnItemAdded(QuestItemSO item) 
    {
        if (!currentCounts.ContainsKey(item)) return;

        currentCounts[item]++;
        UpdateUI();
    }
    private void UpdateUI() 
    {
        string text = $"Цель: {currentQuest.questName}\n";

        foreach (var req in currentQuest.requirements)
        {
            int current = currentCounts[req.item];
            int max = req.requiredStackSize;
            string name = req.item.itemName;
            text += $"{name} : {current}/{max}\n";
        }
        questText.text = text;  
    }
}
