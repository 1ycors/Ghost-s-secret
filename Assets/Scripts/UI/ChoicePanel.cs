using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

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
