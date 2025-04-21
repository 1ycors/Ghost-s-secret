using System;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public static event Action OnInteract;
    public Trigger trigger;
    public DialogueManager dialogueManager;

    private void OnEnable() => InputCustom.OnEPressed += HandleInteraction;
    private void OnDisable() => InputCustom.OnEPressed -= HandleInteraction;

    private void Start()
    {
        trigger = Player.Instance.GetComponent<Trigger>();
    }
    public void HandleInteraction()
    {
        if (dialogueManager == null || dialogueManager.dialogueScript == null)
            return;

        if (dialogueManager.dialogueScript.isDialogueActive)
            return;

        if (trigger.npcDetected)
            OnInteract?.Invoke();
    }
}