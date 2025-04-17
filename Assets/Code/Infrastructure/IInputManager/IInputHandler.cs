using System;
using UnityEngine;

internal interface IInputHandler : IService
{
    event Action<KeyCode, int> ButtonPressed;
    void Update();
}