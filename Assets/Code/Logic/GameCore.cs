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

    private InputManager _inputManager;

    private void Awake()
    {
        ServiceLocator.Register(_palette);

        TableData tableData = new();
        tableData.GeneratePlacement();
        ServiceLocator.Register(tableData);

        GameProcess gameProcess = ServiceLocator.Register<GameProcess>(new());

        _inputManager = ServiceLocator.Register<InputManager>(new());

        ServiceLocator.Register(new ViewManager(_canvasAnimator));

        HandData handData = ServiceLocator.Register(new HandData(tableData, gameProcess, _palette));

        DiceManager diceManager = ServiceLocator.Register(new DiceManager(_palette));

        AnokCashData anokCashData = ServiceLocator.Register(new AnokCashData());

        ServiceLocator.Register(new MarkerData());

        FortuneManager fortuneManager = ServiceLocator.Register(new FortuneManager(_fortunePool, handData, gameProcess, anokCashData));

        ServiceLocator.Register(new ReplaceManager(tableData));

        for (int i = 0; i < _initializable.Length; i++)
        {
            if (_initializable[i] is IInitializable)
                (_initializable[i] as IInitializable).Initialize();
            else
                throw new System.Exception("Массив _initializable нужно заполнять только классами, реализующие IInitializable");
        }

        ServiceLocator.Register(new Game(gameProcess, diceManager, fortuneManager, handData, anokCashData, tableData));
    }

    private void Update()
    {
        _inputManager.Update();
    }
}
