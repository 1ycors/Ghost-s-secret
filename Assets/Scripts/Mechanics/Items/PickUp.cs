using UnityEngine;

public class PickUp : MonoBehaviour
{
    public QuestItem item;
    [SerializeField] private string itemID; //уникальный айди для сохранения состояния

    private bool isPlayerNear;
    private void Start()
    {
        if (GameStateManager.Instance.IsItemPicked(itemID)) // Проверяем, был ли предмет уже подобран ранее
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
            GameStateManager.Instance.MarkItemAsPicked(itemID); // Сохраняем факт подбора
            gameObject.SetActive(false);
        }
    }
}
