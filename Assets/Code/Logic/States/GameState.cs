internal class GameState : IState
{
    private CoreTicker _coreTicker;
    private StateMachine _stateMachine;
    private BuildData _buildData;
    private SceneLoader _sceneLoader;

    public void Enter()
    {
        _sceneLoader.Load("Game", OnLoadCompleted);
    }

    private void OnLoadCompleted()
    {
        
    }

    public void Exit() { }

    public void Update() { }
}