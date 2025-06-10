using System;
using UnityEngine;

public class InteractionController : Singleton<InteractionController>
{
    public Trigger trigger;

    public bool IsInteracting { get; private set; }

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
            UIManager.Instance.description.Continue();
            Debug.Log("Срабатывание Continue из контроллера");
            return;
        }

        if (trigger.currentInteractable != null) 
        {
            trigger.Interact();
            IsInteracting = true;
            Debug.Log("Срабатывание Интеракта из контроллера");
        }
    }
    public void FinishInteraction() 
    {
        IsInteracting = false;
        Debug.Log("Срабатывание FinishInteraction из контроллера");
    }
}