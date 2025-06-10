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
            Debug.Log("������������ Continue �� �����������");
            return;
        }

        if (trigger.currentInteractable != null) 
        {
            trigger.Interact();
            IsInteracting = true;
            Debug.Log("������������ ��������� �� �����������");
        }
    }
    public void FinishInteraction() 
    {
        IsInteracting = false;
        Debug.Log("������������ FinishInteraction �� �����������");
    }
}