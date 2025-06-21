using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Inventory inventory;
    public Description description;
    public ChoicePanel choicePanel;
    public QuestUI questUI;
    public PageUI pageUI;
}
