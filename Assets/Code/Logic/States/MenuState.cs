using System;
using UnityEngine;

internal class MenuState : IState
{
    private CoreTicker _coreTicker;
    private StateMachine _stateMachine;
    private BuildData _buildData;
    private SceneLoader _sceneLoader;
    //новые
    private IInputManager _inputManager;
    private Initializer _initializer;
    private IDataLoader _dataLoader;


    public MenuState(CoreTicker coreTicker, StateMachine stateMachine, BuildData buildData, SceneLoader sceneLoader)
    {
        _coreTicker = coreTicker;
        _stateMachine = stateMachine;
        _buildData = buildData;
        _sceneLoader = sceneLoader;
    }

    public void Enter()
    {
        _sceneLoader.Load("Menu", OnLoadCompleted);
    }

    private void OnLoadCompleted()
    {
        _inputManager = ServiceLocator.Register(new InputManager());
        _dataLoader = ServiceLocator.Register(new ResourcesLoader());

        _initializer = ServiceLocator.Register(new Initializer());
        
        _initializer.InitializeObjects();
        _stateMachine.ChangeState(new GameState());
    }
    public void Update()
    {
        _inputManager?.Update();
    }

    public void Exit() { }

}
