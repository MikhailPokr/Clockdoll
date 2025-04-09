using System;
using UnityEngine;

internal class InputManager : IService
{
    public Action<KeyCode, int> ButtonPresed; // 0 - up, 1 - pressed 
    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            ButtonPresed?.Invoke(KeyCode.LeftArrow, 1);
        if (Input.GetKey(KeyCode.RightArrow))
            ButtonPresed?.Invoke(KeyCode.RightArrow, 1);
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            ButtonPresed?.Invoke(KeyCode.LeftArrow, 0);
        if (Input.GetKeyUp(KeyCode.RightArrow))
            ButtonPresed?.Invoke(KeyCode.RightArrow, 0);
    }
}
