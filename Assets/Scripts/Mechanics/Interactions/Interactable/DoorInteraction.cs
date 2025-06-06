using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction: MonoBehaviour, IInteractable
{
    public QuestItemSO requiredKey;
    private BoxCollider2D cldr;
    private void Start()
    {
        cldr = GetComponentInChildren<BoxCollider2D>();
        cldr.enabled = false;
        if (GameStateManager.Instance.GetDoorState("MaidsRoom"))
            cldr.enabled = true;
    }
    public void Interact()
    {
        SearchKey();
    }
    private bool SearchKey()
    {
        Debug.Log("���� ����...");
        var slots = UIManager.Instance.inventory.slots;
        if (slots == null || slots.Length == 0)
        {
            Debug.LogError("������: ����� ��������� �� �������!");
            InteractionController.Instance.FinishInteraction();
            return false;
        }
        foreach (var slot in slots)
        {
            if (slot.itemInstance == null || slot.itemInstance.itemData == null)
            {
                Debug.Log("����� ���");
                continue;
            }
            if (slot.itemInstance.itemData.itemID == requiredKey.itemID)
            {
                slot.Clear();
                UIManager.Instance.inventory.UpdateInventory();
                GameStateManager.Instance.SetDoorState("MaidsRoom", true);
                cldr.enabled = true;

                Debug.Log("����� �������!");
                InteractionController.Instance.FinishInteraction();
                return true;
            }
        }
        Debug.Log("����� ����");
        InteractionController.Instance.FinishInteraction();
        return false;
    }
}
