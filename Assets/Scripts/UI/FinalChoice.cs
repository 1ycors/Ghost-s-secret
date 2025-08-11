using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalChoice : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI messageText;
    private string questionText = "��� ������?";

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

        if (UIManager.Instance.Inventory.slots.Any(item => item == null))
        {
            maidButton.onClick.AddListener(DefaultEnding);
        }
        else if (UIManager.Instance.Inventory.slots.Any(slot => slot.itemInstance.itemData.itemID == "maidsDiary"))
        {
            maidButton.onClick.AddListener(TrueEnding);
        }
    }
    //����� ���� ����� ����� ������� �������, ����� ����� ������� ����� ���������, ��������, ����� ��� ��� ��������� �����, �� �������, ��� ������ ������� �� ������? ��/�������� ���
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
        Debug.Log("������� �� �������� ��������.");
    }
    private void TrueEnding() 
    {
        Debug.Log("�������� �������� �������!");
        //��������
    }
}
