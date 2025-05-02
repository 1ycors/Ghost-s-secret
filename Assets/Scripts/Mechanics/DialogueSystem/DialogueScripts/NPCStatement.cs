using UnityEngine;

public class NPCStatement : MonoBehaviour
{
    public enum NPCState
    {
        FirstMeeting,
        DefaultState,
        QuestStart,
        QuestComplete
    }
    public NPCState currentState = NPCState.FirstMeeting;

    public DialogueSO firstMeetingDialogue;
    public DialogueSO defaultDialogue;
    public DialogueSO questComplete;
}
