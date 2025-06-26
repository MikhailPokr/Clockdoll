using System;

internal class GameSubStateMachine : IGameSubStateMachine
{
    public GameSubState _currentState;
    public GameSubState CurrentState => _currentState;

    private ClockNum _currentPlaceNumber;
    public ClockNum CurrentPlaceNumber => _currentPlaceNumber;

    public bool IsPedroTurn => _currentState < GameSubState.AnokReaction || _currentState >= GameSubState.PedroReaction;

    public GameSubStateMachine()
    {
        _currentState = GameSubState.PedroStartTurn;
        _currentPlaceNumber = ClockNum.MinValue;
    }

    public void Start() => SignalBus.Publish(new SubStateChangedSignal(_currentState, _currentPlaceNumber));

    public void GoToNextState()
    {
        ClockNum stateIndex = (int)_currentState;

        stateIndex++;

        _currentState = (GameSubState)stateIndex.Value;

        if (_currentState == GameSubState.PedroStartTurn) //Педро вновь ходит, круг замкнулся
        {
            _currentPlaceNumber++;
            SignalBus.Publish(new CircleCompletedSignal());
        }

        SignalBus.Publish(new SubStateChangedSignal(_currentState, _currentPlaceNumber));
    }
}