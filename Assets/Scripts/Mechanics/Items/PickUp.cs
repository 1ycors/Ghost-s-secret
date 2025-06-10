using UnityEngine;

public class PickUp : MonoBehaviour, IInteractable
{
    public QuestItemSO item;
    public string uniqueID; //индивидуальный айдишник

    private void Start()
    {
        if (GameStateManager.Instance.IsItemPicked(uniqueID)) // Проверяем, был ли предмет уже подобран ранее
            gameObject.SetActive(false);
    }
    public void Interact() 
    {
        TryAddItem();
    }
    void TryAddItem()
    {
        if (UIManager.Instance.inventory.AddItem(item))
        {
            GameStateManager.Instance.MarkItemAsPicked(uniqueID); // Сохраняем факт подбора
            gameObject.SetActive(false);
        }
        InteractionController.Instance.FinishInteraction();
    }
}
