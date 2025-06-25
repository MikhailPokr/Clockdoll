using DG.Tweening;
using UnityEngine;

internal class BootstrapState : IState
{
    private CoreTicker _coreTicker;
    private StateMachine _stateMachine;
    //новые 
    private BuildData _buildData;
    private SceneLoader _sceneLoader;

    public BootstrapState(CoreTicker coreTicker, StateMachine stateMachine)
    {
        _coreTicker = coreTicker;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _buildData = ServiceLocator.Register(new BuildData());
        _sceneLoader = ServiceLocator.Register(new SceneLoader(_coreTicker));
        DOTween.Init();

        if (_buildData.Platform == RuntimePlatform.WebGLPlayer)
        {
            IInputHandler inputHandler = ServiceLocator.Register<IInputHandler>(new InputHandler());
            Initializer initializer = ServiceLocator.Register(new Initializer());
            IDataLoader resourcesLoader = ServiceLocator.Register<IDataLoader>(new ResourcesLoader());
            Palette palette = ServiceLocator.Register((Palette)resourcesLoader.LoadPrefab("Palette"));

            _stateMachine.ChangeState(new GameState(
                _coreTicker,
                _stateMachine,
                _buildData,
                _sceneLoader,
                inputHandler,
                initializer,
                resourcesLoader,
                palette
                ));
        }
        else
        {
            _stateMachine.ChangeState(new MenuState(_coreTicker, _stateMachine, _buildData, _sceneLoader));
        }
    }

    public void Exit() { }

    public void Update() { }
}