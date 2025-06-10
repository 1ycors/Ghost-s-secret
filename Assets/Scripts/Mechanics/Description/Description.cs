using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Description : MonoBehaviour
{
    public GameObject window;
    public TMP_Text descriptionText;

    private PlayerMovement playerMovement;
    private DescripSO currentDescrip;
    private Action onComplete;
    private int index;
    private bool isWriting;

    private float writingSpeed = 0.01f;

    private void Awake()
    {
        window.SetActive(false);
    }
    private void Start()
    {
        if (Player.Instance != null)
            playerMovement = Player.Instance.GetComponent<PlayerMovement>();

        if (playerMovement == null)
            Debug.LogError("PlayerMovement не найден!");
    }
    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }
    public void StartDescription(DescripSO newDescrip, Action onComplete = null)
    {
        if (window.activeSelf) return;

        this.onComplete = onComplete;
        currentDescrip = newDescrip;
        index = 0;
        ToggleWindow(true);
        playerMovement.LockMovement();

        ShowCurrentLine();
    }
    private void ShowCurrentLine()
    {
        string line = currentDescrip.descripLines[index];
        StartCoroutine(Writing(line));
    }
    private IEnumerator Writing(string line)
    {
        isWriting = true;
        descriptionText.text = string.Empty;

        foreach (char letter in line)
        {
            descriptionText.text += letter;
            yield return new WaitForSeconds(writingSpeed);
        }
        isWriting = false;
    }
    public void ShowMessage(string message) 
    {
        Debug.Log(message);
    }
    public void Continue()
    {
        if (isWriting) return;

        index++;
        if (index < currentDescrip.descripLines.Length)
        {
            ShowCurrentLine();
        }
        else 
        {
            ToggleWindow(false);
            playerMovement?.UnlockMovement();
            onComplete?.Invoke();
            FinishDescription();
        }
    }
    public void FinishDescription() 
    {
        isWriting = false;
        index = 0;
        isWriting = false;
        InteractionController.Instance.FinishInteraction();
    }
}
