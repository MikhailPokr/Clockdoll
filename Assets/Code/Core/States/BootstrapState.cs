using UnityEngine;

internal class BootstrapState : IState
{
    private CoreTicker _coreTicker;
    private StateMachine _stateMachine;
    //новые 
    private BuildData _buildData;
    private SceneLoader _sceneLoader;

    public BootstrapState(CoreTicker coroutineDispatcher, StateMachine stateMachine)
    {
        _coreTicker = coroutineDispatcher;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _buildData = ServiceLocator.Register(new BuildData());
        _sceneLoader = ServiceLocator.Register(new SceneLoader(_coreTicker));

        if (_buildData.Platform == RuntimePlatform.WebGLPlayer)
        {
            //сразу инициализировать часть сервисов, которые иначе должны быть инициализированны в меню
            InputHandler inputHandler = ServiceLocator.Register(new InputHandler());

            _stateMachine.ChangeState(new GameState());
        }
        else
        {
            _stateMachine.ChangeState(new MenuState(_coreTicker, _stateMachine, _buildData, _sceneLoader));
        }
    }

    public void Exit() { }

    public void Update() { }
}