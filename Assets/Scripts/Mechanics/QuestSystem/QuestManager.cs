using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestSO currentQuest;
    //public Inventory inventory;
    public Trigger trigger;

    private void Start()
    {
        currentQuest.isQuestActive = false;
        currentQuest.isQuestCompleted = false;
    }

    private void OnEnable()
    {
        Trigger.OnInteract += QuestCheck;
    }
    private void OnDisable()
    {
        Trigger.OnInteract -= QuestCheck;
    }

    void QuestStart()
    {
        currentQuest.isQuestActive = true;
        Debug.Log("����� �������!");
    }
    void QuestCheck()
    {
        if (!currentQuest.isQuestActive)
        {
            QuestStart();
            return;
        }
        if (currentQuest.isQuestActive && !currentQuest.isQuestCompleted /*&& RequiredItemSearch()*/)
        {
            QuestComplete();
            return;
        }

        Debug.Log("��������� ������� �� ������!");
    }
    void QuestComplete()
    {
        currentQuest.isQuestActive = false;
        currentQuest.isQuestCompleted = true;
        Debug.Log("����� ��������!");
    }
    //public bool RequiredItemSearch() //�����, ������� ���� ������ ������� � ���������
    //{
    //    if (currentQuest == null)
    //    {
    //        Debug.LogError("������: currentQuest �� �����!");
    //        return false;
    //    }
    //    if (inventory.slots == null || inventory.slots.Length == 0)
    //    {
    //        Debug.LogError("������: ����� ��������� �� �������!");
    //        return false;
    //    }

    //    for (int i = 0; i < inventory.slots.Length; i++)
    //    {
    //        if (inventory.slots[i].isFull && inventory.slots[i].itemName == currentQuest.requiredItem)
    //        {
    //            inventory.slots[i].isFull = false;
    //            inventory.slots[i].itemName = "";
    //            inventory.slots[i].itemIcon = null;

    //            inventory.UpdateInventory();

    //            currentQuest.isQuestCompleted = true;
    //            Debug.Log("������� �����!");
    //            return true;
    //        }
    //    }
    //    return false;
    //}
}
