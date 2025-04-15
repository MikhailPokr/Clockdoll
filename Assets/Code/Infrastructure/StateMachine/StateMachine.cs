internal class StateMachine : IService
{
    private IState _currentState = null;

    public void ChangeState(IState newState)
    {
        if (_currentState != null)
            _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState.Update();
    }
}