using System;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public InventorySlots[] slots;

    public static event Action<QuestItemSO> ItemAdded;
    private void Start()
    {
        slots = GetComponentsInChildren<InventorySlots>();
        UpdateInventory();
    }
    public bool AddItem(QuestItemSO newItem)
    {
        foreach (var slot in slots)
        {
            if (slot.itemInstance != null && slot.itemInstance.itemData == null)
            {
                slot.itemInstance = null;
            }
        }
        Debug.Log("Состояние слотов перед добавлением предмета:");
        foreach (var slot in slots)
        {
            string itemID = slot.itemInstance?.itemData?.itemID ?? "null";
            //Debug.Log($"Слот {slot.name}: itemInstance = {slot.itemInstance != null}, itemID = {itemID}");
        }
        // Пытаемся найти слот с таким же предметом
        foreach (var slot in slots)
        {
            if (slot == null)
            {
                Debug.LogError("Слот в массиве slots равен NULL!");
                continue;
            }
            if (slot.IsFull && slot.itemInstance.itemData.itemID == newItem.itemID)
            {
                slot.itemInstance.stackSize++;
                UpdateInventory();
                ItemAdded?.Invoke(newItem);
                return true;
            }
        }
        //ищем пустой слот
        foreach (var slot in slots)
        {
            if (slot == null)
            {
                Debug.LogError("ОШИБКА: один из элементов массива slots равен NULL!");
                continue;
            }
            if (slot.itemInstance == null)
            {
                slot.itemInstance = new ItemInstance(newItem, 1);
                UpdateInventory();
                ItemAdded?.Invoke(newItem);
                return true;
            }
        }
        Debug.Log("Инвентарь полон.");
        return false;
    }
    public void UpdateInventory()
    {
        foreach (var slot in slots)
        {
            Image slotImage = slot.transform.Find("Icon").GetComponent<Image>(); // создаем переменную slotImage и передаем в нее image слота инвентарь
            if (slot.IsFull && slotImage != null)
            {
                slotImage.sprite = slot.itemInstance.itemData.itemIcon;
                slotImage.enabled = true;
            }
            else if (slotImage != null) 
            {
                slotImage.sprite = null;
                slotImage.enabled = false;
            }
        }
    }
    public void Clear() 
    {
        foreach (var slot in slots)
        {
            slot.Clear();
        }
    }
}
