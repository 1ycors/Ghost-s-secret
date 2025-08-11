using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using static UnityEditor.Progress;

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
        if (GameStateManager.Instance.IsItemPicked(page.uniqueID)) // ���������, ��� �� ������� ��� �������� �����
            gameObject.SetActive(false);
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
            UIManager.Instance.Description.ContinueDescription();
        }
        else
        {
            UIManager.Instance.Description.StartDescription(descripSO, () => TryAddItem());
            inProccess = true;
            UIManager.Instance.Inventory.AddItem(page);
            GameStateManager.Instance.MarkObjectAsInteracted(currentObject, true);
        }
        isObjectMarked = true;
        inProccess = false;
    }
    private void TryAddItem() 
    {
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
