using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Description description;
    [SerializeField] private ChoicePanel choicePanel;
    [SerializeField] private QuestUI questUI;
    [SerializeField] private PageUI pageUI;
    [SerializeField] private ScreenFader screenFader;

    public Inventory Inventory => inventory;
    public Description Description => description;
    public ChoicePanel ChoicePanel => choicePanel;
    public QuestUI QuestUI => questUI;
    public PageUI PageUI => pageUI;
    public ScreenFader ScreenFader => screenFader;
}
