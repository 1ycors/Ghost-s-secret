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
        if (GameStateManager.Instance.IsItemPicked(page.uniqueID)) // Проверяем, был ли предмет уже подобран ранее
            gameObject.SetActive(false);
    }
    public void Interact() 
    {
        if (inProccess)
        {
            UIManager.Instance.description.ContinueDescription();
        }
        if (isObjectMarked)
        {
            Debug.Log("Предмет уже подобран");
            return;
        }
        else
        {
            UIManager.Instance.description.StartDescription(descripSO, () => TryAddItem());
            inProccess = true;
            UIManager.Instance.inventory.AddItem(page);
            GameStateManager.Instance.MarkObjectAsInteracted(currentObject, true);
            isObjectMarked = true;
        }
        InteractionController.Instance.FinishInteraction();
    }
    private void TryAddItem() 
    {
        if (UIManager.Instance.inventory.AddItem(page))
        {
            GameStateManager.Instance.MarkItemAsPicked(page.uniqueID); // Сохраняем факт подбора
            if (page is ReadableQuestItemSO itemSO)
            {
                UIManager.Instance.pageUI.ShowPage(itemSO);
            }
        }
    }
}
