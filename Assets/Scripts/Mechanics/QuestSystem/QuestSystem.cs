using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public QuestSO currentQuest;
    //метод который проверяет сколько страниц найдено и сравнивает с questItemNumber
    //метод вызывается при интеракции с нпс
    //если не находит достаточно страниц, то ничего не происходит и нпс выдает дефолтную реплику
    //как только он находит нужное количество или больше при интеракции с нпс, то квест завершается и начинается катсцена
    public bool RequiredItemSearch() //метод, который ищет нужный предмет в инвентаре
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

        int found = 0; //переменная счетчик. сколько нужных предметов найдено
        foreach (var slot in slots)
        {
            if (slot.itemInstance.itemData.itemName == currentQuest.requiredItem)
            {
                //UIManager.Instance.inventory.slots[i].isFull = false;
                //UIManager.Instance.inventory.slots[i].itemName = "";
                //UIManager.Instance.inventory.slots[i].itemIcon = null;

                //UIManager.Instance.inventory.UpdateInventory();

                //Debug.Log("Предмет отдан!");
                //return true;
                found++;
            }
        }
        return found >= currentQuest.questItemNumber;
    }
}
