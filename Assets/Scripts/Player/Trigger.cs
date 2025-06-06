using System;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public IInteractable currentInteractable;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            currentInteractable = interactable;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable) && interactable == currentInteractable) 
        {
            currentInteractable = null;
        }
    }
    public void Interact() 
    {
        currentInteractable?.Interact();
    }
}
