using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class FireplaceInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private QuestItemSO requiredKey;
    [SerializeField] private GameObject currentObject;
    [SerializeField] private DescripSO firstSO;
    [SerializeField] private DescripSO secondSO;
    [SerializeField] private DescripSO ifYes;
    [SerializeField] private DescripSO ifNo;

    private bool isMarked = false;
    private bool isChoiceActive = false;
    private void Start()
    {
        if (GameStateManager.Instance.IsObjectMarked(currentObject))
            isMarked = true;
    }
    public void Interact() 
    {
        if (InteractionController.Instance.IsInteracting || isChoiceActive)
        {
            Debug.Log("ѕопытка взаимодействовать во врем€ активной интеракции");
            return;
        }
        if (isMarked) 
        {
            UIManager.Instance.Description.StartDescription(secondSO);
        }
        else
        { //скорее всего вот тут проблема. что если поставить условие else if (!isMarked) ?
            UIManager.Instance.Description.StartDescription(firstSO, () =>
            { isChoiceActive = true;
                UIManager.Instance.ChoicePanel.Show("ѕодобрать?", (bool isYes) =>
                    {
                        if (isYes)
                        {
                            UIManager.Instance.Inventory.AddItem(requiredKey);
                            UIManager.Instance.Description.StartDescription(ifYes);
                            GameStateManager.Instance.MarkObjectAsInteracted(currentObject, true);
                            isMarked = true;
                        }
                        else
                        {
                            UIManager.Instance.Description.StartDescription(ifNo);
                        }
                        isChoiceActive = false;
                    });
            });
        }
    }
}
