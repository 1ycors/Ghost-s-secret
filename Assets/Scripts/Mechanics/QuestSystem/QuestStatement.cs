using UnityEngine;

public class QuestStatement : MonoBehaviour
{
    public enum QuestState
    {
        QuestNotActive,
        QuestActive,
        QuestComplete
    }
    public QuestState currentQuest = QuestState.QuestNotActive;
}
