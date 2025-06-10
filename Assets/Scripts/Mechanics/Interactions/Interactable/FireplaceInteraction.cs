using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class FireplaceInteraction : MonoBehaviour, IInteractable
{
    public QuestItemSO requiredKey;
    public DescripSO firstSO;
    public DescripSO secondSO;
    private bool isMarked = false;
    private void Start()
    {
        if (GameStateManager.Instance.IsObjectMarked("Fireplace"))
            isMarked = true;
    }
    public void Interact() 
    {
        if (isMarked) 
        {
            UIManager.Instance.description.StartDescription(secondSO);
            InteractionController.Instance.FinishInteraction();
        }
        else 
        {
            UIManager.Instance.description.StartDescription(firstSO, () =>
                UIManager.Instance.choicePanel.Show("Подобрать?", (bool isYes) =>
                {
                    if (isYes)
                    {
                        UIManager.Instance.inventory.AddItem(requiredKey);
                        UIManager.Instance.description.ShowMessage("Вы подобрали ключ.");
                        GameStateManager.Instance.MarkObjectAsInteracted("Fireplace", true);
                        isMarked = true;
                    }
                    else
                    {
                        UIManager.Instance.description.ShowMessage("Вы решили не трогать предмет.");
                    }
                    InteractionController.Instance.FinishInteraction();
                })
            );
        }
        
    }
}
