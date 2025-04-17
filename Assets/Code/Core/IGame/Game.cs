using Assets.Code.Logic;
using System;
using UnityEngine;

internal class Game : IGame
{
    private IInputHandler _inputHandler;
    private IGameSubStateMachine _gameSubStateMachine;
    private IDollPlacementController _placementController;
    private IDiceController _diceManager;
    private IAnokCashData _cashData;
    private ICardSystem _cardSystem;
    private IDiscardManager _discardManager;
    private IFortuneSystem _fortuneSystem;

    

    public Game(
        IInputHandler inputHandler,
        IGameSubStateMachine gameSubStateMachine,
        IDollPlacementController placementController,
        IDiceController diceManager,
        IAnokCashData cashData,
        ICardSystem cardSystem,
        IDiscardManager discardManager,
        IFortuneSystem fortuneSystem
        )
    {
        _inputHandler = inputHandler;
        _gameSubStateMachine = gameSubStateMachine;
        _placementController = placementController;
        _diceManager = diceManager;
        _cashData = cashData;
        _cardSystem = cardSystem;
        _discardManager = discardManager;
        _fortuneSystem = fortuneSystem;
    }

    public void Start() => _gameSubStateMachine.Start();

    

    #region Старая логика
    /*public Game
        (
        GameProcess gameProcess,
        DiceManager diceManager,
        fortuneSystem fortuneSystem,
        cardSystem cardSystem,
        AnokCashData cashData,
        placementController placementController,
        DiscardManager discardManager,
        inputHandler inputHandler
        )
    {
        _gameProcess = gameProcess;
        _diceManager = diceManager;
        _fortuneSystem = fortuneSystem;
        _cardSystem = cardSystem;
        _cashData = cashData;
        _placementController = placementController;
        _discardManager = discardManager;
        _inputHandler = inputHandler;


        _gameProcess.TurnChanged += OnTurnChanged;
        _cashData.CashOver += OnAnokBankrupt;
        _inputHandler.ButtonPressed += OnButtonPressed;

        _cardSystem.TakeCard(true, 5);
        _cardSystem.TakeCard(false, 5);
    }*/

    private void OnButtonPressed(KeyCode key, int i)
    {
        if (key == KeyCode.Space)
        {
            if (!_gameProcess.ItsPedroTurn)
            {
                // Для хода игрока (Anok) пробел завершает ход
                _gameProcess.OnTurnEnd();
            }
            else
            {
                // Для хода Pedro пробел запускает его ход
                StartPedroTurn();
            }
        }
    }

    private void OnTurnChanged(bool isPedroTurn, int currentPlace)
    {
        _fortuneSystem.GenerateNewList();
        if (isPedroTurn)
        {
            // Ждем нажатия пробела для начала хода Pedro
            // Сам ход теперь запускается в OnButtonPressed
        }
        else
        {
            // Ход Anok начинается автоматически, но завершается по пробелу
            StartAnokTurn();
        }
    }

    private void StartAnokTurn()
    {
        var diceResult = _diceManager.RollDice(12);
        _fortuneSystem.ApplyReward(diceResult[0].value);

        // Не запускаем автоматически карты Pedro, ждем действий игрока
    }

    public void ClickAnokCard(BaseCard card)
    {
        if (_gameProcess.ItsPedroTurn) return;

        if (_discardManager.NeedDiscard(false))
        {
            _discardManager.Discard(card);
            return;
        }

        if (_cardSystem.TryPlayCard(card))
            return;

        // Теперь завершение хода происходит по нажатию пробела
    }

    private void StartPedroTurn()
    {
        var diceResult = _diceManager.RollDice(12);
        _fortuneSystem.ApplyReward(diceResult[0].value);
        PlayPedroCard();
    }

    private void PlayPedroCard()
    {
        var pedroHand = _cardSystem.GetHand(true);

        foreach (var card in pedroHand)
        {
            if (_discardManager.NeedDiscard(false))
            {
                _discardManager.Discard(card);
                continue;
            }

            if (_cardSystem.TryPlayCard(card))
            {
                _gameProcess.OnTurnEnd();
            }
        }
    }

    private void OnAnokBankrupt()
    {
        //поражение
    }

    public void CheckWinCondition()
    {
        bool allDollsCorrect = true;
        for (int i = 1; i <= DollPlacementController.NumberOfPlayers; i++)
        {
            if (_placementController.GetTrueDollPlace(i) != _placementController.DollsCurrentPlace[i])
            {
                allDollsCorrect = false;
                break;
            }
        }

        if (allDollsCorrect)
        {
            //победа 
        }
    }

    public void Dispose()
    {
        _gameProcess.TurnChanged -= OnTurnChanged;
        _cashData.CashOver -= OnAnokBankrupt;
        _inputHandler.ButtonPressed -= OnButtonPressed;
    }
    #endregion
}