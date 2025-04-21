using System;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool npcDetected;

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
