using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public QuestSO currentQuest;

    private void Start()
    {
        currentQuest.isQuestActive = false;
        currentQuest.isQuestCompleted = false;
    }
    void QuestStart()
    {
        currentQuest.isQuestActive = true;
        Debug.Log("����� �������!");
    }
    void QuestComplete()
    {
        currentQuest.isQuestActive = false;
        currentQuest.isQuestCompleted = true;
        Debug.Log("����� ��������!");
    }
    public bool RequiredItemSearch() //�����, ������� ���� ������ ������� � ���������
    {
        if (currentQuest == null)
        {
            Debug.LogError("������: currentQuest �� �����!");
            return false;
        }
        if (UIManager.Instance.inventory.slots == null || UIManager.Instance.inventory.slots.Length == 0)
        {
            Debug.LogError("������: ����� ��������� �� �������!");
            return false;
        }

        for (int i = 0; i < UIManager.Instance.inventory.slots.Length; i++)
        {
            if (UIManager.Instance.inventory.slots[i].isFull && UIManager.Instance.inventory.slots[i].itemName == currentQuest.requiredItem)
            {
                UIManager.Instance.inventory.slots[i].isFull = false;
                UIManager.Instance.inventory.slots[i].itemName = "";
                UIManager.Instance.inventory.slots[i].itemIcon = null;

                UIManager.Instance.inventory.UpdateInventory();

                currentQuest.isQuestCompleted = true;
                Debug.Log("������� �����!");
                return true;
            }
        }
        return false;
    }
}
