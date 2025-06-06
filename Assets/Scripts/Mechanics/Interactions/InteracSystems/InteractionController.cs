using System;
using UnityEngine;

public class InteractionController : Singleton<InteractionController>
{
    public Trigger trigger;
    public bool IsInteracting { get; private set; }
    //public DialogueManager dialogueManager;

    private void OnEnable() => InputCustom.OnEPressed += HandleInteraction;
    private void OnDisable() => InputCustom.OnEPressed -= HandleInteraction;

    private void Start()
    {
        trigger = Player.Instance.GetComponent<Trigger>();
    }
    public void HandleInteraction()
    {
        if (IsInteracting)
            return;

        if (trigger.currentInteractable != null) {
            trigger.Interact();
            IsInteracting = true;
            Debug.Log("Срабатывание Интеракта из контроллера");
        }
    }
    public void FinishInteraction() 
    {
        IsInteracting = false;
    }
}