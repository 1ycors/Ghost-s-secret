using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForQuest : MonoBehaviour
{
    [SerializeField] private Collider2D coldr;
    private void Start()
    {
        coldr = GetComponentInChildren<BoxCollider2D>();
        coldr.enabled = false;
        if (GameStateManager.Instance.GetDoorState("MainRoom"))
            coldr.enabled = true;
    }
    private void OnEnable()
    {
        NPCInteractionManager.OnQuestEnable += ColliderActivate;
    }
    private void Disenable()
    {
        NPCInteractionManager.OnQuestEnable -= ColliderActivate;
    }
    private void ColliderActivate() 
    {
        GameStateManager.Instance.SetDoorState("MainRoom", true);
        coldr.enabled = true;
    }
}
