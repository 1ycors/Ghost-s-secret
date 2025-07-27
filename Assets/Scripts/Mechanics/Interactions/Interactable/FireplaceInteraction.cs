using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class FireplaceInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private QuestItemSO requiredKey;
    [SerializeField] private DescripSO firstSO;
    [SerializeField] private DescripSO secondSO;
    [SerializeField] private GameObject currentObject;

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
            Debug.Log("������� ����������������� �� ����� �������� ����������");
            return;
        }
        if (isMarked) 
        {
            UIManager.Instance.description.StartDescription(secondSO);
        }
        else
        {
            isChoiceActive = true;
            UIManager.Instance.description.StartDescription(firstSO, () =>
                UIManager.Instance.choicePanel.Show("���������?", (bool isYes) =>
                {
                    if (isYes)
                    {
                        UIManager.Instance.inventory.AddItem(requiredKey);
                        UIManager.Instance.description.ShowMessage("�� ��������� ����.");
                        GameStateManager.Instance.MarkObjectAsInteracted(currentObject, true);
                        isMarked = true;
                    }
                    else
                    {
                        UIManager.Instance.description.ShowMessage("�� ������ �� ������� �������.");
                    }
                    isChoiceActive = false;
                    InteractionController.Instance.FinishInteraction();
                })
            );
        }
    }
}
