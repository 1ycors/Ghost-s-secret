using System;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public static event Action OnInteract;
    private bool npcDetected;
    public DialogueManager dialogueManager;

    private void OnEnable() => InputCustom.OnEPressed += HandleInteraction;
    private void OnDisable() => InputCustom.OnEPressed -= HandleInteraction;

    public void HandleInteraction() 
    {
        if (dialogueManager == null || dialogueManager.dialogueScript == null)
            return;

        if (dialogueManager.dialogueScript.isDialogueActive)
            return;

        if (npcDetected)
            OnInteract?.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC")) 
        {
            npcDetected = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC")) 
        {
            npcDetected = false;
        }
    }
}
