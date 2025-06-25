﻿using System;
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
    private LocalizationHandler _localizationHandler;
    private ITextHandler _textHandler;
    private IDialogueBoxController _dialogueBoxController; 
    private Palette _palette;
    private Canvas _canvas;
    private IMainMenuController _mainMenuController;

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
        _inputHandler = ServiceLocator.Register<IInputHandler>(new InputHandler());
        _initializer = ServiceLocator.Register(new Initializer());   
        _dataLoader = ServiceLocator.Register<IDataLoader>(new ResourcesLoader());
        _palette = ServiceLocator.Register((Palette)_dataLoader.LoadPrefab("Palette"));
        _localizationHandler = ServiceLocator.Register(new LocalizationHandler());
        _textHandler = ServiceLocator.Register<ITextHandler>(new TextHandler(_dataLoader, _localizationHandler));

        _canvas = GameObject.FindAnyObjectByType<Canvas>();

        _localizationHandler.ChangeLocalizationKey("ru");

        _dialogueBoxController = ServiceLocator.Register<IDialogueBoxController>(new DialogueBoxController(_dataLoader, _localizationHandler, _textHandler, _palette, _canvas));
        _mainMenuController = ServiceLocator.Register<IMainMenuController>(new MainMenuController(_localizationHandler));

        _mainMenuController.OnGameStart += ChangeStateToGame;

        _initializer.InitializeObjects();
    }

    private void ChangeStateToGame()
    {
        _stateMachine.ChangeState(new GameState(
            _coreTicker,
            _stateMachine,
            _buildData,
            _sceneLoader,
            _inputHandler,
            _initializer,
            _dataLoader,
            _palette
            ));
    }
    public void Update()
    {
        _inputHandler?.Update();
    }

    public void Exit() { }

}