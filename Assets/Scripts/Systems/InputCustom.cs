using System;
using UnityEngine;

public class InputCustom : Singleton<InputCustom>
{
    public static event Action OnEPressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            OnEPressed?.Invoke();
    }
}
