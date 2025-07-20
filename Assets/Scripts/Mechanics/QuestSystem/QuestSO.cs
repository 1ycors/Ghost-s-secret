using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest/QuestSO")]
public class QuestSO : ScriptableObject
{
    [TextArea(1, 5)]
    public string questName;

    [System.Serializable]
    public class QuestRequirement
    {
        public QuestItemSO item;
        public string itemID;
        public int requiredStackSize;
        public bool isOptional = false;
    }
    public List<QuestRequirement> requirements = new List<QuestRequirement>();
}
