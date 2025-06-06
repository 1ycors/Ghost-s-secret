using UnityEngine;

public class PickUp : MonoBehaviour, IInteractable
{
    public QuestItemSO item;
    //private bool isPlayerNear;
    public string uniqueID; //�������������� ��������

    private void Start()
    {
        if (GameStateManager.Instance.IsItemPicked(uniqueID)) // ���������, ��� �� ������� ��� �������� �����
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
            GameStateManager.Instance.MarkItemAsPicked(uniqueID); // ��������� ���� �������
            gameObject.SetActive(false);
        }
    }
    //public bool IsTheSameItem(QuestItemSO other) 
    //{
    //    return this.item.itemID == other.itemID;
    //}
}
