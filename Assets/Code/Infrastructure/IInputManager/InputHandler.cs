using UnityEngine;

internal class InputHandler : IInputHandler
{
    public void Update()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
            SignalBus.Publish(new InputSignal(KeyCode.LeftArrow, 1));
        if (Input.GetKey(KeyCode.RightArrow))
            SignalBus.Publish(new InputSignal(KeyCode.RightArrow, 1));
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            SignalBus.Publish(new InputSignal(KeyCode.LeftArrow, 0));
        if (Input.GetKeyUp(KeyCode.RightArrow))
            SignalBus.Publish(new InputSignal(KeyCode.RightArrow, 0));
        if (Input.GetKeyUp(KeyCode.Space))
            SignalBus.Publish(new InputSignal(KeyCode.Space, 0));

    }
}
