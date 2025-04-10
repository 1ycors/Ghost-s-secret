using System;
using UnityEngine;

public class InputCustom : MonoBehaviour
{
    public static event Action OnInterañtPressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            OnInterañtPressed?.Invoke();
    }
}
