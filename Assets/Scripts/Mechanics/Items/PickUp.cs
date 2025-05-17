using UnityEngine;

public class PickUp : MonoBehaviour
{
    public QuestItemSO item;
    private bool isPlayerNear;
    public string uniqueID; //�������������� ��������

    private void Start()
    {
        if (GameStateManager.Instance.IsItemPicked(uniqueID)) // ���������, ��� �� ������� ��� �������� �����
            gameObject.SetActive(false);
    }
    private void OnEnable() => InputCustom.OnEPressed += CheckUp;
    private void OnDisable() => InputCustom.OnEPressed -= CheckUp;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = false;
    }
    void CheckUp()
    {
        if (isPlayerNear)
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
    public bool IsTheSameItem(QuestItemSO other) 
    {
        return this.item.itemID == other.itemID;
    }
}
