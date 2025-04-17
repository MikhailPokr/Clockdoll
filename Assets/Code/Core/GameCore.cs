using Assets.Code.Logic;
using System.Collections.Generic;
using UnityEngine;

internal class GameCore : MonoBehaviour
{
    [SerializeField] private Palette _palette;
    [SerializeField] private FortunePool _fortunePool;
    [Space]
    [SerializeField] private MonoBehaviour[] _initializable;
    [Space]
    [SerializeField] private Animator _canvasAnimator;

    private InputHandler _inputManager;

    private void Awake()
    {
        ServiceLocator.Register(_palette);

        DollPlacementController placementController = new();
        placementController.Generate();
        ServiceLocator.Register(placementController);

        GameProcess gameProcess = ServiceLocator.Register<GameProcess>(new());

        _inputManager = ServiceLocator.Register<InputHandler>(new());

        ServiceLocator.Register(new ProjectionController(_canvasAnimator));

        CardSystem cardSystem = ServiceLocator.Register(new CardSystem(placementController, gameProcess, _palette));

        DiceManager diceManager = ServiceLocator.Register(new DiceManager(_palette));

        AnokCashData anokCashData = ServiceLocator.Register(new AnokCashData());

        ServiceLocator.Register(new NoteMarkerData());

        DiscardManager discardManager = ServiceLocator.Register(new DiscardManager(cardSystem));

        FortuneManager fortuneManager = ServiceLocator.Register(new FortuneManager(_fortunePool, cardSystem, gameProcess, anokCashData, discardManager));

        ServiceLocator.Register(new ReplaceManager(placementController));

        Game game = ServiceLocator.Register(new Game(gameProcess, diceManager, fortuneManager, cardSystem, anokCashData, placementController, discardManager, _inputManager));

        for (int i = 0; i < _initializable.Length; i++)
        {
            if (_initializable[i] is IInitializable)
                (_initializable[i] as IInitializable).Initialize();
            else
                throw new System.Exception("Массив _initializable нужно заполнять только классами, реализующие IInitializable");
        }

        gameProcess.Start();
    }

    private void Update()
    {
        _inputManager.Update();
    }
}
