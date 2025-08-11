using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] private QuestSO currentQuest;
    [SerializeField] private QuestItemSO itemSO;
    public bool RequiredItemsSearch()
    {
        if (currentQuest == null)
        {
            Debug.LogError("Ошибка: currentQuest не задан!");
            return false;
        }

        var slots = UIManager.Instance.inventory.slots;
        if (slots == null || slots.Length == 0)
        {
            Debug.LogError("Ошибка: слоты инвентаря не найдены!");
            return false;
        }
        foreach (var requirement in currentQuest.requirements)
        {
            if (requirement.isOptional)
                continue; //пропускаем необязательные требования

            bool matchFound = false;

            foreach (var slot in slots)
            {
                var item = slot.itemInstance?.itemData;

                if (item == null)
                    continue;

                if (item.itemID == requirement.itemID && slot.itemInstance.stackSize >= requirement.requiredStackSize)
                {
                    matchFound = true;
                    break;
                }
            }
            if (!matchFound)
                return false; //если не нашли обязательный предмет - квест не может быть завершен
        }
        return true;
    }
}
