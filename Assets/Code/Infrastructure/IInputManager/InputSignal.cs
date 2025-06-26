using UnityEngine;

internal struct InputSignal : ISignal
{
    public KeyCode KeyCode { get; }
    public int PressState { get; }

    public InputSignal(KeyCode keyCode, int pressState)
    {
        KeyCode = keyCode;
        PressState = pressState;
    }
}