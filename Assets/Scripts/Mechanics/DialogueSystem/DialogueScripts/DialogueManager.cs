using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NPCStatement;

public class DialogueManager : MonoBehaviour
{
    public NPCStatement npcStatement;
    public Dialogue dialogueScript;
    public void PlayDialogue()
    {
        if (dialogueScript.isDialogueActive) return;

        switch (npcStatement.currentState)
        {
            case NPCState.FirstMeeting:
                dialogueScript.StartDialogue(npcStatement.firstMeetingDialogue);
                npcStatement.currentState = NPCState.DefaultState;
                break;
            case NPCState.QuestStart:
                dialogueScript.StartDialogue(npcStatement.defaultDialogue);
                break;
            case NPCState.QuestComplete:
                dialogueScript.StartDialogue(npcStatement.questComplete);
                break;
            case NPCState.DefaultState:
                dialogueScript.StartDialogue(npcStatement.defaultDialogue);
                break;
        }
    }
}
