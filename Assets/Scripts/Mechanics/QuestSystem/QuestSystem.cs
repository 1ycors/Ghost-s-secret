using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public QuestSO currentQuest;
    public QuestItemSO itemSO;
    //берем айди предмета из слота и сравниваем его с SO и количеством страниц
    //то есть словарь на словарь
    public bool RequiredItemsSearch() //метод, который ищет нужный предмет в инвентаре
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

        //int found = 0; //переменная счетчик. сколько нужных предметов найдено
        //foreach (var slot in slots)
        //{
        //    if (/*slot.itemInstance.itemData.itemName == currentQuest.requirements.*/)
        //    {
        //        //UIManager.Instance.inventory.slots[i].isFull = false;
        //        //UIManager.Instance.inventory.slots[i].itemName = "";
        //        //UIManager.Instance.inventory.slots[i].itemIcon = null;

        //        //UIManager.Instance.inventory.UpdateInventory();

        //        //Debug.Log("Предмет отдан!");
        //        //return true;
        //        found++;
        //    }
        //}
        //return found >= currentQuest.requirements.Count;
        //foreach (var slot in slots)
        //{
        //    if (slot.itemInstance.itemData.itemID == itemSO.itemID && slot.itemInstance.stackSize == currentQuest.requirements.Count)
        //    {
        //        found++;
        //    }
        //}
        //return found >= currentQuest.requirements.Count;
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

                if (item == requirement.item && slot.itemInstance.stackSize >= requirement.requiredStackSize)
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
