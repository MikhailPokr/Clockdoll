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

    private void GoToNextState()
    {
        int stateIndex = (int)_currentState;

        stateIndex = stateIndex == 12 ? 1 : stateIndex + 1;

        _currentState = (GameSubState)stateIndex;

        if (_currentState == GameSubState.PedroStartTurn) //Педро вновь ходит, круг замкнулся
        {

        }

        _currentPlaceNumber++;

        SubStateChanged?.Invoke(_currentState, stateIndex);
    }
}