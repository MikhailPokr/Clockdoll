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
    private IDollPlacementController _placementController;
    private IProjectionController _projectionController;
    private ICardSystem _cardSystem;


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
        _placementController = ServiceLocator.Register(new DollPlacementController());
        _placementController.Generate();

        _projectionController = ServiceLocator.Register(new ProjectionController(GameObject.FindAnyObjectByType<Canvas>().GetComponent<Animator>()));
        _cardSystem = ServiceLocator.Register(new CardSystem(_placementController, _palette));
    }

    public void Exit() { }

    public void Update() { }
}