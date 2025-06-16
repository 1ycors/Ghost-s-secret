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
            Debug.Log("������� ����������������� �� ����� �������� ����������");
            return;
        }
        Debug.Log("������������ Interact � DescripObject");
        UIManager.Instance.description.StartDescription(descripSO);
    }
}
