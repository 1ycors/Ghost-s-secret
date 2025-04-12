using System;
using UnityEngine;

public class InputCustom : MonoBehaviour
{
    public static event Action OnEPressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            OnEPressed?.Invoke();
    }
}
