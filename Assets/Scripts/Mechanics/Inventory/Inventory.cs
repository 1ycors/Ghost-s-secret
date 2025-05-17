using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public InventorySlots[] slots = new InventorySlots[6];

    private void Start()
    {
        slots = GetComponentsInChildren<InventorySlots>();
        UpdateInventory();
    }
    public bool AddItem(QuestItemSO newItem)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].isFull && slots[i].storedItem != null && slots[i].storedItem.itemID == newItem.itemID)
            {
                slots[i].storedItem.stackSize++;
                UpdateInventory();
                return true;
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].isFull) //���� ���� ����
            {
                slots[i].itemIcon = newItem.itemIcon;
                slots[i].itemName = newItem.itemName;
                slots[i].isFull = true;

                UpdateInventory();
                return true;
            }
        }
        Debug.Log("��������� �����!");
        return false;
    }
    public void UpdateInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Image slotImage = slots[i].transform.Find("Icon").GetComponent<Image>(); // ������� ���������� slotImage � �������� � ��� image ����� ���������

            if (slots[i].isFull && slotImage != null)
            {
                slotImage.sprite = slots[i].itemIcon; //����� ������ ����� ��������� � ����������� �� ������ ��������
                slotImage.enabled = true;
            }
            else if (slotImage != null) //���� �� ������ � ���� ����, ������ ���� ����, � ������ ������� ������
            {
                slotImage.sprite = null;
                slotImage.enabled = false;
            }
        }
    }
}
