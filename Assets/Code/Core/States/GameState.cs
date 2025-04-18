using Assets.Code.Logic;
using UnityEngine;

internal class GameState : IState
{
    private CoreTicker _coreTicker;
    private StateMachine _stateMachine;
    private BuildData _buildData;
    private SceneLoader _sceneLoader;
    private IInputHandler _inputHandler;
    private Initializer _initializer;
    private IDataLoader _dataLoader;
    private Palette _palette;
    //новые
    private IGameSubStateMachine _gameSubStateMachine;
    private IDollPlacementController _placementController;
    private IDiceController _diceController;
    private IAnokCashData _anokCashData;
    private IProjectionController _projectionController;
    private INoteMarkerData _noteMarkerData;
    private IReplaceManager _replaceManager;
    private ICardSystem _cardSystem;
    private IDiscardManager _discardManager;
    private FortunePool _fortunePool;
    private IFortuneSystem _fortuneSystem;
    private IGame _game;

    public GameState(
        CoreTicker coreTicker,
        StateMachine stateMachine,
        BuildData buildData,
        SceneLoader sceneLoader,
        IInputHandler inputHandler,
        Initializer initializer, 
        IDataLoader dataLoader,
        Palette palette
        )
    {
        _coreTicker = coreTicker;
        _stateMachine = stateMachine;
        _buildData = buildData;
        _sceneLoader = sceneLoader;
        _inputHandler = inputHandler;
        _initializer = initializer;
        _dataLoader = dataLoader;
        _palette = palette;
    }

    public void Enter()
    {
        _sceneLoader.Load("Game", OnLoadCompleted);
    }

    private void OnLoadCompleted()
    {
        _gameSubStateMachine = ServiceLocator.Register(new GameSubStateMachine()); 

        _placementController = ServiceLocator.Register(new DollPlacementController()); 
        _placementController.Generate();

        _diceController = ServiceLocator.Register(new DiceController(_palette)); 
        _anokCashData = ServiceLocator.Register(new AnokCashData(100)); //можно вынести значение в SO 

        _projectionController = ServiceLocator.Register(new ProjectionController(GameObject.FindAnyObjectByType<Canvas>().GetComponent<Animator>())); 
        _noteMarkerData =  ServiceLocator.Register(new NoteMarkerData()); 
        _replaceManager = ServiceLocator.Register(new ReplaceManager(_placementController)); // проверить InsertDoll

        _cardSystem = ServiceLocator.Register(new CardSystem(_placementController, _gameSubStateMachine, _palette)); 
        _discardManager = ServiceLocator.Register(new DiscardManager(_cardSystem)); 

        _fortunePool = ServiceLocator.Register((FortunePool)_dataLoader.LoadPrefab("FortunePool"));
        _fortuneSystem = ServiceLocator.Register(new FortuneSystem(_fortunePool, _cardSystem, _gameSubStateMachine, _anokCashData, _discardManager, 12)); //можно вынести значение в SO 

        _game = ServiceLocator.Register(new Game
        (
        _inputHandler,
        _gameSubStateMachine,
        _placementController,
        _diceController,
        _anokCashData, 
        _cardSystem,
        _discardManager, 
        _fortuneSystem
        ));

        _initializer.InitializeObjects();

        //_game.Start();
    }
    public void Update()
    {
        _inputHandler.Update();
    }

    public void Exit() { }

}