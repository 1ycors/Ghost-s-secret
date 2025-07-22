using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageUI : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private TMP_Text pageText;
    public Button closeButton;
    private void Awake()
    {
        window.SetActive(false);
    }
    private void Start()
    {
        closeButton.onClick.RemoveAllListeners();

        closeButton.onClick.AddListener(ClosePage);
    }
    public void ShowPage(ReadableQuestItemSO itemSO) 
    {
        window.SetActive(true);
        pageText.text = itemSO.readableText;
        Player.Instance.LockMovement();
    }
    public void ClosePage() 
    {
        window.SetActive(false);
        pageText.text = "";
        Player.Instance.UnlockMovement();
    }
}
