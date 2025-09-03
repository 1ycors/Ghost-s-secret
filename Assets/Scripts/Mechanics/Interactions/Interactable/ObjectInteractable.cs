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
            Debug.Log("Предмет уже подобран");
            return;
        }
        if (inProccess)
        {
            Debug.Log("Срабатывание блока if(inProccess) из ObjectInteractable");
            UIManager.Instance.Description.ContinueDescription();
        }
        else
        {
            Debug.Log("Срабатывание блока else");
            inProccess = true;
            UIManager.Instance.Description.StartDescription(descripSO, () =>
            {
                TryAddThisItem();
                Debug.Log("Срабатывание Action из ObjectInteractable");
                inProccess = false;
            });
            GameStateManager.Instance.MarkObjectAsInteracted(currentObject, true);
        }
        isObjectMarked = true;
    }
    private void TryAddThisItem() 
    {
        Debug.Log("Срабатывание TryAddItem из ObjectInteractable");
        if (UIManager.Instance.Inventory.AddItem(page))
        {
            GameStateManager.Instance.MarkItemAsPicked(page.uniqueID); // Сохраняем факт подбора
            if (page is ReadableQuestItemSO itemSO)
            {
                UIManager.Instance.PageUI.ShowPage(itemSO);
            }
        }
    }
}
