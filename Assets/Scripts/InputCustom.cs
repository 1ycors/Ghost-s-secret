using System;
using UnityEngine;

public class InputCustom : MonoBehaviour
{
    public static event Action OnIntera�tPressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            OnIntera�tPressed?.Invoke();
    }
}
