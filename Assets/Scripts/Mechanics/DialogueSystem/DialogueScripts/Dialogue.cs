using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField]private GameObject window;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text npcName;

    private DialogueSO currentDialogue; // храним текущий диалог (firstmeeting или default)

    private int index;
    private bool waitForNext;
    public bool IsDialogueActive { get; private set; }

    private float writingSpeed = 0.01f;
    private void Awake()
    {
        window.SetActive(false);
    }
    private void Start()
    {
        InteractionController.OnContinueDialogue += TryAdvanceDialogue;
    }
    private void OnDestroy()
    {
        InteractionController.OnContinueDialogue -= TryAdvanceDialogue;
    }
    //private void OnEnable() => InputCustom.OnEPressed += TryAdvanceDialogue;
    //private void OnDisable() => InputCustom.OnEPressed -= TryAdvanceDialogue;

    void TryAdvanceDialogue() 
    {
        if (!IsDialogueActive)
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
        if (IsDialogueActive || dialogue == null) return;

        InteractionController.Instance.StartInteraction();

        IsDialogueActive = true;
        currentDialogue = dialogue;
        index = 0;

        npcName.text = currentDialogue.npcName;
        ToggleWindow(true);
        GetDialogue(index);

        if (IsDialogueActive)
            Player.Instance.LockMovement();
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
        IsDialogueActive = false;

        InteractionController.Instance.FinishInteraction();

        if (!IsDialogueActive)
            Player.Instance.UnlockMovement();
    }
}
