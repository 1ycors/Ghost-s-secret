using UnityEngine;
[CreateAssetMenu(fileName = "New Quest Item", menuName = "Inventory/QuestItem")]
public class QuestItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public string itemID; //���� ��� ���������/������
    public int stackSize = 1;
}
