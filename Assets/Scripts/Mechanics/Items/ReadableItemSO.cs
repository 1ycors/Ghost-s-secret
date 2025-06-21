using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Readable Quest Item", menuName = "Inventory/ReadableQuestItem")]
public class ReadableQuestItemSO : QuestItemSO
{
    [TextArea(3, 10)]
    public string readableText;
}