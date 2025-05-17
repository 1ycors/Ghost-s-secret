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
            if (!slots[i].isFull) //если слот пуст
            {
                slots[i].itemIcon = newItem.itemIcon;
                slots[i].itemName = newItem.itemName;
                slots[i].isFull = true;

                UpdateInventory();
                return true;
            }
        }
        Debug.Log("Инвентарь полон!");
        return false;
    }
    public void UpdateInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Image slotImage = slots[i].transform.Find("Icon").GetComponent<Image>(); // создаем переменную slotImage и передаем в нее image слота инвентаря

            if (slots[i].isFull && slotImage != null)
            {
                slotImage.sprite = slots[i].itemIcon; //берем спрайт слота инвентаря и присваиваем ей иконку предмета
                slotImage.enabled = true;
            }
            else if (slotImage != null) //если мы попали в этот блок, значит слот пуст, а значит убираем иконку
            {
                slotImage.sprite = null;
                slotImage.enabled = false;
            }
        }
    }
}
