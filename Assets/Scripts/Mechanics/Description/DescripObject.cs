using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescripObject : MonoBehaviour, IInteractable
{
    public DescripSO descripSO;
    public void Interact() 
    {
        Debug.Log("������������ Interact � DescripObject");
        UIManager.Instance.description.StartDescription(descripSO);
        InteractionController.Instance.FinishInteraction();
    }
}
