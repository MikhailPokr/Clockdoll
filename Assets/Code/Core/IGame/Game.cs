using Assets.Code.Logic;
using System;
using UnityEngine;

internal class Game : IGame
{
    private DiceManager _diceManager;
    private FortuneManager _fortuneManager;
    private CardSystem _cardSystem;
    private AnokCashData _cashData;
    private DollPlacementController _placementController;
    private DiscardManager _discardManager;
    private InputHandler _inputManager;

    public GameSubState _currentState;
    public GameSubState CurrentState => _currentState;

    private ClockNum _currentPlaceNumber;
    public ClockNum CurrentPlaceNumber => _currentPlaceNumber;

    public Action CircleCompleted;

    public Action<GameSubState, ClockNum> SubStateChanged;

    public Game(
        DiceManager diceManager,
        FortuneManager fortuneManager,
        CardSystem cardSystem,
        AnokCashData cashData,
        DollPlacementController placementController,
        DiscardManager discardManager,
        InputHandler inputManager
        )
    {
        _diceManager = diceManager;
        _fortuneManager = fortuneManager;
        _cardSystem = cardSystem;
        _cashData = cashData;
        _placementController = placementController;
        _discardManager = discardManager;
        _inputManager = inputManager;
    }

    public void Start()
    {
        _currentState = GameSubState.PedroStartTurn;
        _currentPlaceNumber = ClockNum.MinValue;

        SubStateChanged?.Invoke(_currentState, CurrentPlaceNumber);
    }

    private void GoToNextState()
    {
        int stateIndex = (int)_currentState;

        stateIndex = stateIndex == 12 ? 1 : stateIndex + 1;

        _currentState = (GameSubState)stateIndex;

        if (_currentState == GameSubState.PedroStartTurn) //Педро вновь ходит, круг замкнулся
        {

        }

        _currentPlaceNumber++;

        SubStateChanged?.Invoke(_currentState, stateIndex);
    }

    #region Старая логика
    /*public Game
        (
        GameProcess gameProcess,
        DiceManager diceManager,
        FortuneManager fortuneManager,
        cardSystem cardSystem,
        AnokCashData cashData,
        placementController placementController,
        DiscardManager discardManager,
        InputManager inputManager
        )
    {
        _gameProcess = gameProcess;
        _diceManager = diceManager;
        _fortuneManager = fortuneManager;
        _cardSystem = cardSystem;
        _cashData = cashData;
        _placementController = placementController;
        _discardManager = discardManager;
        _inputManager = inputManager;


        _gameProcess.TurnChanged += OnTurnChanged;
        _cashData.CashOver += OnAnokBankrupt;
        _inputManager.ButtonPressed += OnButtonPressed;

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
        _fortuneManager.GenerateNewList();
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
        _fortuneManager.ApplyReward(diceResult[0].value);

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
        _fortuneManager.ApplyReward(diceResult[0].value);
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
        _inputManager.ButtonPressed -= OnButtonPressed;
    }
    #endregion
}