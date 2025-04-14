using UnityEngine;

public class PickUp : MonoBehaviour
{
    public QuestItem item;
    [SerializeField] private string itemID; //���������� ���� ��� ���������� ���������

    private bool isPlayerNear;
    private void Awake()
    { // ���������, ��� �� ������� ��� �������� �����
        if (GameStateManager.IsItemPicked(itemID))
            gameObject.SetActive(false);
    }
    private void OnEnable() => InputCustom.OnEPressed += CheckUp;
    private void OnDisable() => InputCustom.OnEPressed -= CheckUp;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
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
            GameStateManager.MarkItemAsPicked(itemID); // ��������� ���� �������
            gameObject.SetActive(false);
        }
    }
}
