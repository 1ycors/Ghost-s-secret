using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalChoice : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI messageText;
    private string questionText = "Кто убийца?";

    public Button fatherButton;
    public Button motherButton;
    public Button brotherButton;
    public Button maidButton;

    public CutsceneManager cutsceneManager;

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

        if (UIManager.Instance.inventory.slots.Any(item => item == null))
        {
            Debug.Log("пропуск");
        }
        else if (UIManager.Instance.inventory.slots.Any(slot => slot.itemInstance.itemData.itemID == "maidsDiary"))
        {
            maidButton.onClick.AddListener(TrueEnding);
        }
        else
        {
            maidButton.onClick.AddListener(DefaultEnding);
        }
    }
    //перед этим нужно будет сделать кортину, чтобы перед выбором вышло сообщение, например, перед тем как завершить квест, вы уверены, что готовы указать на убийцу? Да/Подумать еще
    public void StartFinalChoice() 
    {
        Debug.Log("СРАБАТЫВАЕНИЕ StartFinalChoice ИЗ ФАЙНАЛ ЧОЙС БЛЯТЬ");
        panel.SetActive(true);
        messageText.text = questionText;
    }
    public void FinishFinalChoice() 
    {
        panel.SetActive(false);
    }
    private void DefaultEnding() 
    {
        //запускаем катсцену с неистенной концовкой
        cutsceneManager.StartDefaultCutscene();
        FinishFinalChoice();
        Debug.Log("Открыта не истинная концвока.");
    }
    private void TrueEnding() 
    {
        //запускаем катсцену с истинной концовкой
        Debug.Log("Истинная концвока открыта!");
        //катсцена
    }
}
