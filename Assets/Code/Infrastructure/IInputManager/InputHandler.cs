using System;
using UnityEngine;

internal class InputHandler : IInputHandler
{
    public Action<KeyCode, int> ButtonPressed; // 0 - up, 1 - pressed 
    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            ButtonPressed?.Invoke(KeyCode.LeftArrow, 1);
        if (Input.GetKey(KeyCode.RightArrow))
            ButtonPressed?.Invoke(KeyCode.RightArrow, 1);
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            ButtonPressed?.Invoke(KeyCode.LeftArrow, 0);
        if (Input.GetKeyUp(KeyCode.RightArrow))
            ButtonPressed?.Invoke(KeyCode.RightArrow, 0);
        if (Input.GetKeyUp(KeyCode.Space))
            ButtonPressed?.Invoke(KeyCode.Space, 0);
    }
}
