using System;

internal class GameSubStateMachine : IGameSubStateMachine
{
    public GameSubState _currentState;
    public GameSubState CurrentState => _currentState;

    private ClockNum _currentPlaceNumber;
    public ClockNum CurrentPlaceNumber => _currentPlaceNumber;

    public event Action CircleCompleted;

    public event Action<GameSubState, ClockNum> SubStateChanged;

    public GameSubStateMachine()
    {
        _currentState = GameSubState.PedroStartTurn;
        _currentPlaceNumber = ClockNum.MinValue;
    }

    public void Start() => SubStateChanged?.Invoke(_currentState, CurrentPlaceNumber);

    public void GoToNextState()
    {
        ClockNum stateIndex = (int)_currentState;

        stateIndex++;

        _currentState = (GameSubState)stateIndex.Value;

        if (_currentState == GameSubState.PedroStartTurn) //Педро вновь ходит, круг замкнулся
        {
            _currentPlaceNumber++;
        }

        SubStateChanged?.Invoke(_currentState, _currentPlaceNumber);
    }
}