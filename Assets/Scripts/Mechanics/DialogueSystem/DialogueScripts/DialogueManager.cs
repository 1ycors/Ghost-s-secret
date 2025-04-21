using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NPCStatement;

public class DialogueManager : MonoBehaviour
{
    public NPCStatement npcStatement;
    public Dialogue dialogueScript;

    private void OnEnable() => InteractionController.OnInteract += PlayDialogue;
    private void OnDisable() => InteractionController.OnInteract -= PlayDialogue;

    public void PlayDialogue()
    {
        if (dialogueScript.isDialogueActive) return;

        switch (npcStatement.currentState)
        {
            case NPCState.FirstMeeting:
                dialogueScript.StartDialogue(npcStatement.firstMeetingDialogue);
                npcStatement.currentState = NPCState.Default;
                break;
            case NPCState.QuestStart:
                dialogueScript.StartDialogue(npcStatement.defaultDialogue);
                break;
            case NPCState.QuestComplete:
                dialogueScript.StartDialogue(npcStatement.questComplete);
                npcStatement.currentState = NPCState.Default;
                break;
            case NPCState.Default:
                dialogueScript.StartDialogue(npcStatement.defaultDialogue);
                break;
        }
    }
}
