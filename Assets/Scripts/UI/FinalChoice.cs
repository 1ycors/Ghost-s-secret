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
    private string questionText = "��� ������?";

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
            Debug.Log("�������");
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
    //����� ���� ����� ����� ������� �������, ����� ����� ������� ����� ���������, ��������, ����� ��� ��� ��������� �����, �� �������, ��� ������ ������� �� ������? ��/�������� ���
    public void StartFinalChoice() 
    {
        Debug.Log("������������� StartFinalChoice �� ������ ���� �����");
        panel.SetActive(true);
        messageText.text = questionText;
    }
    public void FinishFinalChoice() 
    {
        panel.SetActive(false);
    }
    private void DefaultEnding() 
    {
        //��������� �������� � ���������� ���������
        cutsceneManager.StartDefaultCutscene();
        FinishFinalChoice();
        Debug.Log("������� �� �������� ��������.");
    }
    private void TrueEnding() 
    {
        //��������� �������� � �������� ���������
        Debug.Log("�������� �������� �������!");
        //��������
    }
}
