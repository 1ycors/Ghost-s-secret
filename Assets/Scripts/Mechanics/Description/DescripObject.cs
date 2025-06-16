using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescripObject : MonoBehaviour, IInteractable
{
    public DescripSO descripSO;
    public void Interact() 
    {
        if (InteractionController.Instance.IsInteracting)
        {
            Debug.Log("Попытка взаимодействовать во время активной интеракции");
            return;
        }
        Debug.Log("Срабатывание Interact в DescripObject");
        UIManager.Instance.description.StartDescription(descripSO);
    }
}
