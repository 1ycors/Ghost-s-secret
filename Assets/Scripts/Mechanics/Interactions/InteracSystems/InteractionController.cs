using System;
using UnityEngine;

public class InteractionController : Singleton<InteractionController>
{
    [SerializeField] private Trigger trigger;
    [SerializeField] private Dialogue dialogue;
    public bool IsInteracting { get; private set; }
    public static event Action OnContinueDescription;
    public static event Action OnContinueDialogue;

    private void OnEnable() => InputCustom.OnEPressed += HandleInteraction;
    private void OnDisable() => InputCustom.OnEPressed -= HandleInteraction;

    private void Start()
    {
        trigger = Player.Instance.GetComponent<Trigger>();
    }
    public void HandleInteraction()
    {
        if (IsInteracting)
        {
            if (dialogue == null)
                dialogue = FindAnyObjectByType<Dialogue>(FindObjectsInactive.Include);

            if (dialogue != null && dialogue.IsDialogueActive)
            {
                Debug.Log("Âûחûגאועסÿ OnContinueDialogue");
                OnContinueDialogue?.Invoke();
                return;
            }
            else
            {
                Debug.Log("Âûחûגאועסÿ OnContinueDescription");
                OnContinueDescription?.Invoke();
                return;
            }
            
        }
        if (trigger.currentInteractable != null) 
        {
            trigger.Interact();
        }
    }
    public void StartInteraction() 
    {
        IsInteracting = true;
        Debug.Log("IsInteracting = true");
    }
    public void FinishInteraction() 
    {
        IsInteracting = false;
        Debug.Log("IsInteracting = false");
    }
}