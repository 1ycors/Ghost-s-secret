using System;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceInteraction : MonoBehaviour, IInteractable
{
    public QuestItemSO requiredKey;
    public DescripSO descripSO;
    public void Interact() 
    {
        UIManager.Instance.description.StartDescription(descripSO);
        UIManager.Instance.choicePanel.Show("Подобрать?", (bool isYes) =>
        {
            if (isYes)
            {
                UIManager.Instance.inventory.AddItem(requiredKey);
                UIManager.Instance.description.ShowMessage("Вы подобрали ключ.");
            }
            else
            {
                UIManager.Instance.description.ShowMessage("Вы решили не трогать предмет.");
            }
            InteractionController.Instance.FinishInteraction();
        });
    }
}
