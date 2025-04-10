using UnityEngine;

public class NPCStatement : MonoBehaviour
{
    public enum NPCState
    {
        FirstMeeting,
        Default,
        QuestStart,
        QuestInProgress,
        QuestComplete
    }
    public NPCState currentState = NPCState.FirstMeeting;

    public DialogueSO firstMeetingDialogue;
    public DialogueSO defaultDialogue;
    public DialogueSO questStart;
    public DialogueSO questComplete;
}
