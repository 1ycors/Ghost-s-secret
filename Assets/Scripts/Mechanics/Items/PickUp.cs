using UnityEngine;

public class PickUp : MonoBehaviour, IInteractable
{
    [SerializeField] private QuestItemSO item;

    private void Start()
    {
        if (GameStateManager.Instance.IsItemPicked(item.uniqueID)) // Проверяем, был ли предмет уже подобран ранее
            gameObject.SetActive(false);
    }
    public void Interact() 
    {
        TryAddItem();
    }
    private void TryAddItem()
    {
        Debug.Log("Срабатывание TryAddItem из PickUp");
        if (UIManager.Instance.Inventory.AddItem(item))
        {
            GameStateManager.Instance.MarkItemAsPicked(item.uniqueID); // Сохраняем факт подбора
            gameObject.SetActive(false);
            if (item is ReadableQuestItemSO itemSO)
            {
                UIManager.Instance.PageUI.ShowPage(itemSO);
            }
        }
        InteractionController.Instance.FinishInteraction();
    }
}
