using System;

internal interface IGameSubStateMachine : IService
{
    ClockNum CurrentPlaceNumber { get; }
    GameSubState CurrentState { get; }
    bool IsPedroTurn { get; }

    event Action CircleCompleted;
    event Action<GameSubState, ClockNum> SubStateChanged;

    void Start();
    void GoToNextState();
}