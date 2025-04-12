using UnityEngine;
[CreateAssetMenu(fileName = "New Quest Item", menuName = "Inventory/QuestItem")]
public class QuestItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
}
