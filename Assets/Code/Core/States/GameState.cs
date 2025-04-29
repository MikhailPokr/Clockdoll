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
    private DialogueSystem _dialogueSystem;
    private IGame _game;
    private IAnokPlayer _anokPlayer;
    private IPedroPlayer _pedroPlayer;

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
        _gameSubStateMachine = ServiceLocator.Register<IGameSubStateMachine>(new GameSubStateMachine()); 

        _placementController = ServiceLocator.Register<IDollPlacementController>(new DollPlacementController()); 
        _placementController.Generate();

        _diceController = ServiceLocator.Register<IDiceController>(new DiceController(_palette)); 
        _anokCashData = ServiceLocator.Register<IAnokCashData>(new AnokCashData(100)); //можно вынести значение в SO 

        Canvas canvas = GameObject.FindAnyObjectByType<Canvas>();

        _projectionController = ServiceLocator.Register<IProjectionController>(new ProjectionController(canvas.GetComponent<Animator>())); 
        _noteMarkerData =  ServiceLocator.Register<INoteMarkerData>(new NoteMarkerData()); 
        _replaceManager = ServiceLocator.Register<IReplaceManager>(new ReplaceManager(_placementController)); // проверить InsertDoll

        _cardSystem = ServiceLocator.Register<ICardSystem>(new CardSystem(_placementController, _gameSubStateMachine, _palette)); 
        _discardManager = ServiceLocator.Register<IDiscardManager>(new DiscardManager(_cardSystem)); 

        _fortunePool = ServiceLocator.Register((FortunePool)_dataLoader.LoadPrefab("FortunePool"));
        _fortuneSystem = ServiceLocator.Register<IFortuneSystem>(new FortuneSystem(_fortunePool, _cardSystem, _gameSubStateMachine, _anokCashData, _discardManager, 12)); //можно вынести значение в SO 

        _dialogueSystem = ServiceLocator.Register<DialogueSystem>(new DialogueSystem(_dataLoader, _coreTicker, canvas, _palette));

        _game = ServiceLocator.Register<IGame>(new Game
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

        _anokPlayer = ServiceLocator.Register(new AnokPlayer(_game, _gameSubStateMachine));
        _pedroPlayer = ServiceLocator.Register(new PedroPlayer(_game, _gameSubStateMachine));

        _game.Start();
    }
    public void Update()
    {
        _inputHandler.Update();
    }

    public void Exit() { }

}