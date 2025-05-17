using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest/QuestSO")]
public class QuestSO : ScriptableObject
{
    [TextArea(1, 5)]
    public string questName;
    public string requiredItem;
    public int questItemNumber = 3;
}
