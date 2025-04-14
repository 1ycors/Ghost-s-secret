using System.Collections;
using TMPro;
using UnityEngine;

public class Description : MonoBehaviour
{
    public GameObject window;
    public TMP_Text descriptionText;

    private PlayerMovement playerMovement;
    private DescripSO currentDescrip;

    private float writingSpeed = 0.01f;
    public bool isPanelActive;
    private int index;
    private bool waitForNext;

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
    //private void OnEnable() => PickUp.OnPlayerInteraction += TryActivedPanel;
    //private void OnDisable() => PickUp.OnPlayerInteraction -= TryActivedPanel;

    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }
    private void TryActivedPanel()
    {
        if (!isPanelActive)
        {
            PanelActive();
        }
        else if (waitForNext)
        {
            waitForNext = false;
            index++;

            if (index < currentDescrip.descripLines.Length)
                GetDescrip(index);
            else
                PanelDisactive();
        }
    }
    private void PanelActive() 
    {
        ToggleWindow(true);
        isPanelActive = true;
        index = 0;

        GetDescrip(index);
        if (isPanelActive)
            playerMovement.LockMovement();

    }
    private void GetDescrip(int i) 
    {
        string line = currentDescrip.descripLines[i];
        descriptionText.text = string.Empty;
        StartCoroutine(Writing(line));
    }
    IEnumerator Writing(string line)
    {
        foreach (char letter in line)
        {
            descriptionText.text += letter;
            yield return new WaitForSeconds(writingSpeed);
        }
        waitForNext = true;
    }
    private void PanelDisactive() 
    {
        isPanelActive = false;
        ToggleWindow(false);

        if (!isPanelActive)
            playerMovement.UnlockMovement();
    }
}
