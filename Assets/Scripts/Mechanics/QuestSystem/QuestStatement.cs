using UnityEngine;

public class QuestStatement : MonoBehaviour
{
    public enum QuestState
    {
        QuestNotActive,
        QuestActive,
        QuestInProgress,
        QuestComplete
    }

    public QuestState currentQuest = QuestState.QuestNotActive;
}
