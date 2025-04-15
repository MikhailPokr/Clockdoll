using Assets.Code.Logic;
using System;
using UnityEngine;

internal class Game : IGame
{
    private GameProcess _gameProcess;
    private DiceManager _diceManager;
    private FortuneManager _fortuneManager;
    private HandData _handData;
    private AnokCashData _cashData;
    private TableData _tableData;
    private DiscardManager _discardManager;
    private InputManager _inputManager;



    #region Старая логика
    public Game
        (
        GameProcess gameProcess,
        DiceManager diceManager,
        FortuneManager fortuneManager,
        HandData handData,
        AnokCashData cashData,
        TableData tableData,
        DiscardManager discardManager,
        InputManager inputManager
        )
    {
        _gameProcess = gameProcess;
        _diceManager = diceManager;
        _fortuneManager = fortuneManager;
        _handData = handData;
        _cashData = cashData;
        _tableData = tableData;
        _discardManager = discardManager;
        _inputManager = inputManager;

        //чтобы не работало ничего

        /*_gameProcess.TurnChanged += OnTurnChanged;
        _cashData.CashOver += OnAnokBankrupt;
        _inputManager.ButtonPressed += OnButtonPressed;

        _handData.TakeCard(true, 5);
        _handData.TakeCard(false, 5);*/
    }

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

        if (_handData.TryPlayCard(card))
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
        var pedroHand = _handData.GetHand(true);

        foreach (var card in pedroHand)
        {
            if (_discardManager.NeedDiscard(false))
            {
                _discardManager.Discard(card);
                continue;
            }

            if (_handData.TryPlayCard(card))
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
        for (int i = 1; i <= TableData.NumberOfPlayers; i++)
        {
            if (_tableData.GetTrueDollPlace(i) != _tableData.DollsCurrentPlace[i])
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