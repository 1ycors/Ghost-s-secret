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
    private bool isChoiceActive = false;
    private void Start()
    {
        if (GameStateManager.Instance.IsObjectMarked("Fireplace"))
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
                        GameStateManager.Instance.MarkObjectAsInteracted("Fireplace", true);
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
