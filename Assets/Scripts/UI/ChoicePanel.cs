using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanel : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI messageText;
    public Button yesButton;
    public Button noButton;

    private Action<bool> callback;
    private void Start()
    {
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        yesButton.onClick.AddListener(() => { callback?.Invoke(true); panel.SetActive(false); });
        noButton.onClick.AddListener(() => { callback?.Invoke(false); panel.SetActive(false); });

        panel.SetActive(false);
    }
    public void Show(string question, Action<bool> onChoice) 
    {
        messageText.text = question;
        callback = onChoice;
        panel.SetActive(true);
    }
}
