internal interface IGameSubStateMachine : IService
{
    ClockNum CurrentPlaceNumber { get; }
    GameSubState CurrentState { get; }
    bool IsPedroTurn { get; }

    void Start();
    void GoToNextState();
}