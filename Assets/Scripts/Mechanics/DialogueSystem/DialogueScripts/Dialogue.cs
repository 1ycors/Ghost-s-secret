using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public GameObject window;
    public TMP_Text dialogueText;
    public TMP_Text npcName;

    private PlayerMovement playerMovement;
    private DialogueSO currentDialogue; // ������ ������� ������ (firstmeeting ��� default)

    private int index;
    private bool waitForNext;
    public bool isDialogueActive;

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
            Debug.LogError("PlayerMovement �� ������!");

        InteractionController.onContinue += TryAdvanceDialogue;
    }
    private void OnDestroy()
    {
        InteractionController.onContinue -= TryAdvanceDialogue;
    }
    private void OnEnable() => InputCustom.OnEPressed += TryAdvanceDialogue;
    private void OnDisable() => InputCustom.OnEPressed -= TryAdvanceDialogue;

    void TryAdvanceDialogue() 
    {
        if (!isDialogueActive)
            return;

        if (waitForNext)
        {
            waitForNext = false;
            index++;

            if (index < currentDialogue.dialogueLines.Length)
                GetDialogue(index);
            else
                EndDialogue();
        }
    }
    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }
    public void StartDialogue(DialogueSO dialogue)
    {
        if (isDialogueActive || dialogue == null) return;

        InteractionController.Instance.StartInteraction();

        isDialogueActive = true;
        currentDialogue = dialogue;
        index = 0;

        npcName.text = currentDialogue.npcName;
        ToggleWindow(true);
        GetDialogue(index);

        if (isDialogueActive)
            playerMovement.LockMovement();
    }
    private void GetDialogue(int i)
    {       
        string line = currentDialogue.dialogueLines[i];
        dialogueText.text = string.Empty;
        StartCoroutine(Writing(line));
    }
    IEnumerator Writing(string line)
    {
        foreach (char letter in line)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(writingSpeed);
        }
        waitForNext = true;
    }
    public void EndDialogue()
    {
        ToggleWindow(false);
        waitForNext = false;
        isDialogueActive = false;

        InteractionController.Instance.FinishInteraction();

        if (!isDialogueActive)
            playerMovement.UnlockMovement();
    }
}
