using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalChoice : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI messageText;
    private string questionText = "Кто убийца?";

    [SerializeField] private Button fatherButton;
    [SerializeField] private Button motherButton;
    [SerializeField] private Button brotherButton;
    [SerializeField] private Button maidButton;

    [SerializeField] private CutsceneManager cutsceneManager;

    private void Awake()
    {
        panel.SetActive(false);
    }
    private void Start()
    {
        motherButton.onClick.RemoveAllListeners();
        fatherButton.onClick.RemoveAllListeners();
        brotherButton.onClick.RemoveAllListeners();
        maidButton.onClick.RemoveAllListeners();

        motherButton.onClick.AddListener(DefaultEnding);
        fatherButton.onClick.AddListener(DefaultEnding);
        brotherButton.onClick.AddListener(DefaultEnding);

        var slots = UIManager.Instance.Inventory?.slots;

        if (slots == null)
            return;

        if (slots.Any(slot => slot?.itemInstance.itemData?.itemID == "MaidsDiary"))
            maidButton.onClick.AddListener(TrueEnding);
        else
            maidButton.onClick.AddListener(DefaultEnding);
    }
    private void OnEnable()
    {
        NPCInteractionManager.OnQuestComplete += StartFinalChoice;
    }
    private void OnDisable()
    {
        NPCInteractionManager.OnQuestComplete -= StartFinalChoice;
    }
    public void StartFinalChoice()
    {
        panel.SetActive(true);
        messageText.text = questionText;
    }
    public void FinishFinalChoice() 
    {
        panel.SetActive(false);
    }
    private void DefaultEnding() 
    {
        cutsceneManager.StartDefaultCutscene();
        FinishFinalChoice();
        Debug.Log("Открыта не истинная концвока.");
    }
    private void TrueEnding() 
    {
        cutsceneManager.StartTrueCutscene();
        FinishFinalChoice();
        Debug.Log("Истинная концвока открыта!");
    }
}
