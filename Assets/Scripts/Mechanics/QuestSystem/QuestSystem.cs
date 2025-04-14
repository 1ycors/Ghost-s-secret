using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public QuestSO currentQuest;

    private void Start()
    {
        currentQuest.isQuestActive = false;
        currentQuest.isQuestCompleted = false;
    }
    void QuestStart()
    {
        currentQuest.isQuestActive = true;
        Debug.Log("Квест начался!");
    }
    void QuestComplete()
    {
        currentQuest.isQuestActive = false;
        currentQuest.isQuestCompleted = true;
        Debug.Log("Квест завершен!");
    }
    public bool RequiredItemSearch() //метод, который ищет нужный предмет в инвентаре
    {
        if (currentQuest == null)
        {
            Debug.LogError("Ошибка: currentQuest не задан!");
            return false;
        }
        if (UIManager.Instance.inventory.slots == null || UIManager.Instance.inventory.slots.Length == 0)
        {
            Debug.LogError("Ошибка: слоты инвентаря не найдены!");
            return false;
        }

        for (int i = 0; i < UIManager.Instance.inventory.slots.Length; i++)
        {
            if (UIManager.Instance.inventory.slots[i].isFull && UIManager.Instance.inventory.slots[i].itemName == currentQuest.requiredItem)
            {
                UIManager.Instance.inventory.slots[i].isFull = false;
                UIManager.Instance.inventory.slots[i].itemName = "";
                UIManager.Instance.inventory.slots[i].itemIcon = null;

                UIManager.Instance.inventory.UpdateInventory();

                currentQuest.isQuestCompleted = true;
                Debug.Log("Предмет отдан!");
                return true;
            }
        }
        return false;
    }
}
