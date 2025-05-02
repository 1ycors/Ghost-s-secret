using UnityEngine;

public class QuestStatement : MonoBehaviour
{
    public enum QuestState
    {
        QuestNotActive,
        QuestActive,
        QuestComplete,
        AllQuestsDone
    }
    public QuestState currentQuest = QuestState.QuestNotActive;
}
