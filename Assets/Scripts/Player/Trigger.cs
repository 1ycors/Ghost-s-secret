using System;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public static event Action OnInteract;
    private bool npcDetected;

    private void OnEnable()
    {
        InputCustom.OnIntera�tPressed += HandleInteraction;
    }
    private void OnDisable() => InputCustom.OnIntera�tPressed -= HandleInteraction;

    private void HandleInteraction() 
    {
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
