using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction: MonoBehaviour
{
    public QuestItemSO requiredKey;
    private bool playerDetected;
    private BoxCollider2D cldr;
    private void Start()
    {
        cldr = GetComponentInChildren<BoxCollider2D>();
        cldr.enabled = false;
        if (GameStateManager.Instance.GetDoorState("MaidsRoom"))
            cldr.enabled = true;
    }
    private void OnEnable() => InputCustom.OnEPressed += TryOpenTheDoor;
    private void OnDisable() => InputCustom.OnEPressed -= TryOpenTheDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerDetected = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerDetected = false;
    }
    private void TryOpenTheDoor() 
    {
        if (playerDetected)
            SearchItem();
    }
    public bool SearchItem()
    {
        Debug.Log("Ищем ключ...");
        var slots = UIManager.Instance.inventory.slots;
        if (slots == null || slots.Length == 0)
        {
            Debug.LogError("Ошибка: слоты инвентаря не найдены!");
            return false;
        }
        foreach (var slot in slots)
        {
            if (slot.itemInstance == null || slot.itemInstance.itemData == null)
            {
                Debug.Log("Ключа нет");
                continue;
            }

            if (slot.itemInstance.itemData.itemID == requiredKey.itemID)
            {
                slot.Clear();
                UIManager.Instance.inventory.UpdateInventory();
                GameStateManager.Instance.SetDoorState("MaidsRoom", true);
                cldr.enabled = true;

                Debug.Log("Дверь открыта!");
                return true;
            }
        }
        Debug.Log("Нужен ключ");
        return false;
    }
}
