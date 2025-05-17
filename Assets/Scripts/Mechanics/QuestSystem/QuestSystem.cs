using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public QuestSO currentQuest;
    //����� ������� ��������� ������� ������� ������� � ���������� � questItemNumber
    //����� ���������� ��� ���������� � ���
    //���� �� ������� ���������� �������, �� ������ �� ���������� � ��� ������ ��������� �������
    //��� ������ �� ������� ������ ���������� ��� ������ ��� ���������� � ���, �� ����� ����������� � ���������� ��������
    public bool RequiredItemSearch() //�����, ������� ���� ������ ������� � ���������
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

        int found = 0; //���������� �������. ������� ������ ��������� �������
        for (int i = 0; i < slots.Length; i++)
        {
            var slot = slots[i]; //��� ���� ���������� ���������� ���� �������� �������� ������
            if (slots[i].isFull && slots[i].itemName == currentQuest.requiredItem)
            {
                //UIManager.Instance.inventory.slots[i].isFull = false;
                //UIManager.Instance.inventory.slots[i].itemName = "";
                //UIManager.Instance.inventory.slots[i].itemIcon = null;

                //UIManager.Instance.inventory.UpdateInventory();

                //Debug.Log("������� �����!");
                //return true;
                found++;
            }
        }
        return found >= currentQuest.questItemNumber;
    }
}
