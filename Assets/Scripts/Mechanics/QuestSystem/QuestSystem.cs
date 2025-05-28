using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public QuestSO currentQuest;
    public QuestItemSO itemSO;
    //����� ���� �������� �� ����� � ���������� ��� � SO � ����������� �������
    //�� ���� ������� �� �������
    public bool RequiredItemsSearch() //�����, ������� ���� ������ ������� � ���������
    {
        if (currentQuest == null)
        {
            Debug.LogError("������: currentQuest �� �����!");
            return false;
        }

        var slots = UIManager.Instance.inventory.slots;
        if (slots == null || slots.Length == 0)
        {
            Debug.LogError("������: ����� ��������� �� �������!");
            return false;
        }

        //int found = 0; //���������� �������. ������� ������ ��������� �������
        //foreach (var slot in slots)
        //{
        //    if (/*slot.itemInstance.itemData.itemName == currentQuest.requirements.*/)
        //    {
        //        //UIManager.Instance.inventory.slots[i].isFull = false;
        //        //UIManager.Instance.inventory.slots[i].itemName = "";
        //        //UIManager.Instance.inventory.slots[i].itemIcon = null;

        //        //UIManager.Instance.inventory.UpdateInventory();

        //        //Debug.Log("������� �����!");
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
                continue; //���������� �������������� ����������

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
                return false; //���� �� ����� ������������ ������� - ����� �� ����� ���� ��������
        }
        return true;
    }
}
