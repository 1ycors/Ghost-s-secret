using UnityEngine;

public class PickUp : MonoBehaviour
{
    public QuestItem item;
    public Inventory inventory;

    private bool isPlayerNear;

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
        {
            TryAddItem();
        }
    }
    void TryAddItem()
    {
        if (inventory.AddItem(item))
        {
            gameObject.SetActive(false);
        }
    }
}
