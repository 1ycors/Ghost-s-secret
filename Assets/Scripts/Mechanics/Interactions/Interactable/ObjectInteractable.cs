using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private QuestItemSO page;
    [SerializeField] private DescripSO descripSO;
    [SerializeField] private GameObject currentObject;

    private bool isObjectMarked = false;
    private bool inProccess = false;
    private void Start()
    {
        if (GameStateManager.Instance.IsObjectMarked(currentObject))
            isObjectMarked = true;
    }
    public void Interact() 
    {
        if (isObjectMarked)
        {
            Debug.Log("������� ��� ��������");
            return;
        }
        if (inProccess)
        {
            Debug.Log("������������ ����� if(inProccess) �� ObjectInteractable");
            UIManager.Instance.Description.ContinueDescription();
        }
        else
        {
            Debug.Log("������������ ����� else");
            inProccess = true;
            UIManager.Instance.Description.StartDescription(descripSO, () =>
            {
                TryAddThisItem();
                Debug.Log("������������ Action �� ObjectInteractable");
                inProccess = false;
            });
            GameStateManager.Instance.MarkObjectAsInteracted(currentObject, true);
        }
        isObjectMarked = true;
    }
    private void TryAddThisItem() 
    {
        Debug.Log("������������ TryAddItem �� ObjectInteractable");
        if (UIManager.Instance.Inventory.AddItem(page))
        {
            GameStateManager.Instance.MarkItemAsPicked(page.uniqueID); // ��������� ���� �������
            if (page is ReadableQuestItemSO itemSO)
            {
                UIManager.Instance.PageUI.ShowPage(itemSO);
            }
        }
    }
}
