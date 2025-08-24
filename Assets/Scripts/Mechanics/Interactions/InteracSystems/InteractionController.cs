using System;
using UnityEngine;

public class InteractionController : Singleton<InteractionController>
{
    [SerializeField] private Trigger trigger;
    public bool IsInteracting { get; private set; }
    public static event Action OnContinue;

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
            OnContinue?.Invoke();
            return;
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