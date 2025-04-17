using System;
using UnityEngine;

internal class MenuState : IState
{
    private CoreTicker _coreTicker;
    private StateMachine _stateMachine;
    private BuildData _buildData;
    private SceneLoader _sceneLoader;
    //новые
    private IInputHandler _inputHandler;
    private Initializer _initializer;
    private IDataLoader _dataLoader;
    private Palette _palette;


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
        _inputHandler = ServiceLocator.Register(new InputHandler());
        _dataLoader = ServiceLocator.Register(new ResourcesLoader());

        _initializer = ServiceLocator.Register(new Initializer());
        _palette = ServiceLocator.Register((Palette)_dataLoader.LoadPrefab("Palette"));
        
        _initializer.InitializeObjects();
        _stateMachine.ChangeState(new GameState());
    }
    public void Update()
    {
        _inputHandler?.Update();
    }

    public void Exit() { }

}
