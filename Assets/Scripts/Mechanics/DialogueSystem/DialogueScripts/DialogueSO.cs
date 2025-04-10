using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/DialogueSO")]
public class DialogueSO : ScriptableObject
{
    [TextArea(1, 5)]
    public string[] dialogueLines;
    public string npcName;

}
