using System;
using UnityEngine;

public class InteractionController : Singleton<InteractionController>
{
    public Trigger trigger;
    public bool IsInteracting { get; private set; }
    public static event Action onContinue;

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
            onContinue?.Invoke();
            return;
        }

        if (trigger.currentInteractable != null) 
        {
            trigger.Interact();
            Debug.Log("Срабатывание Интеракта из контроллера");
        }
    }
    public void StartInteraction() 
    {
        IsInteracting = true;
        Debug.Log("Срабатывание StartInteraction из контроллера");
    }
    public void FinishInteraction() 
    {
        IsInteracting = false;
        Debug.Log("Срабатывание FinishInteraction из контроллера");
    }
}