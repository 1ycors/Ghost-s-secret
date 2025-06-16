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
                Debug.LogWarning($"���� {slot.name} �������� ������ itemInstance. ������.");
                slot.itemInstance = null;
            }
        }
        Debug.Log("��������� ������ ����� ����������� ��������:");
        foreach (var slot in slots)
        {
            string itemID = slot.itemInstance?.itemData?.itemID ?? "null";
            Debug.Log($"���� {slot.name}: itemInstance = {slot.itemInstance != null}, itemID = {itemID}");
        }
        // �������� ����� ���� � ����� �� ���������
        foreach (var slot in slots)
        {
            if (slot == null)
            {
                Debug.LogError("���� � ������� slots ����� NULL!");
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
        //���� ������ ����
        foreach (var slot in slots)
        {
            if (slot == null)
            {
                Debug.LogError("������: ���� �� ��������� ������� slots ����� NULL!");
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
        Debug.Log("��������� �����.");
        return false;
    }
    public void UpdateInventory()
    {
        foreach (var slot in slots)
        {
            Image slotImage = slot.transform.Find("Icon").GetComponent<Image>(); // ������� ���������� slotImage � �������� � ��� image ����� ���������
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
}
