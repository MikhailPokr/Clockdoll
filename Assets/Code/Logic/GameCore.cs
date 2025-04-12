using System.Collections.Generic;
using UnityEngine;

internal class GameCore : MonoBehaviour
{
    [SerializeField] private Palette _palette;
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

        ServiceLocator.Register(new HandData(tableData, gameProcess, _palette));

        ServiceLocator.Register(new DiceManager(_palette));


        for (int i = 0; i < _initializable.Length; i++)
        {
            if (_initializable[i] is IInitializable)
                (_initializable[i] as IInitializable).Initialize();
            else
                throw new System.Exception("ћассив _initializable нужно заполн€ть только классами, реализующие IInitializable");
        }
    }

    private void Update()
    {
        _inputManager.Update();

        //далее тестовые проверки нажатий, они будут убраны
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ServiceLocator.Resolve<GameProcess>().OnTurnEnd();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ServiceLocator.Resolve<HandData>().TakeCard(false, 2);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ServiceLocator.Resolve<HandData>().TakeCard(true, 8);
            print("ѕедро получает 8 карт");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ServiceLocator.Resolve<HandData>().SwitchHand();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            List<BaseCard> hand = ServiceLocator.Resolve<HandData>().GetHand(true);
            ServiceLocator.Resolve<HandData>().PlayCard(hand[Random.Range(0, hand.Count)]);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            ServiceLocator.Resolve<DiceManager>().RollDice(8, 10, 16);
        }
    }
}
