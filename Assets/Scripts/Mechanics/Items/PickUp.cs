using UnityEngine;

public class PickUp : MonoBehaviour, IInteractable
{
    public QuestItemSO item;

    private void Start()
    {
        if (GameStateManager.Instance.IsItemPicked(item.uniqueID)) // ���������, ��� �� ������� ��� �������� �����
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
            GameStateManager.Instance.MarkItemAsPicked(item.uniqueID); // ��������� ���� �������
            gameObject.SetActive(false);
            if (item is ReadableQuestItemSO itemSO)
            {
                UIManager.Instance.pageUI.ShowPage(itemSO);
            }
        }
        InteractionController.Instance.FinishInteraction();
    }
}
