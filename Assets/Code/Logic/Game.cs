using System;

internal class Game : IService
{
    private GameProcess _gameProcess;
    private DiceManager _diceManager;
    private FortuneManager _fortuneManager;
    private HandData _handData;
    private AnokCashData _cashData;
    private TableData _tableData;

    public Game
        (
        GameProcess gameProcess, 
        DiceManager diceManager, 
        FortuneManager fortuneManager, 
        HandData handData, 
        AnokCashData cashData, 
        TableData tableData
        )
    {
        _gameProcess = gameProcess;
        _diceManager = diceManager;
        _fortuneManager = fortuneManager;
        _handData = handData;
        _cashData = cashData;
        _tableData = tableData;

        _gameProcess.TurnChanged += OnTurnChanged;
        _cashData.CashOver += OnAnokBankrupt;

        _handData.TakeCard(true, 5);
        _handData.TakeCard(false, 5);
    }
    private void OnTurnChanged(bool isPedroTurn, int currentPlace)
    {
        if (isPedroTurn)
        {
            StartPedroTurn();
        }
        else
        {
            StartAnokTurn();
        }
    }

    private void StartAnokTurn()
    {
        var diceResult = _diceManager.RollDice(12); 

        _fortuneManager.ApplyReward(diceResult[0].value);

        PlayPedroCard();
    }

    public void PlayAnokCard(BaseCard card)
    {
        if (_gameProcess.ItsPedroTurn) return;

        if (card.CheckCondition())
        {
            card.PlayEffect();
            _handData.PlayCard(card);
            _gameProcess.OnTurnEnd();
        }
    }

    private void PlayPedroCard()
    {
        var pedroHand = _handData.GetHand(true);

        foreach (var card in pedroHand)
        {
            if (card.CheckCondition())
            {
                card.PlayEffect();
                _handData.PlayCard(card);
                break;
            }
        }
    }

    private void StartPedroTurn()
    {
        var pedroHand = _handData.GetHand(true);

        foreach (var card in pedroHand)
        {
            if (card.CheckCondition())
            {
                card.PlayEffect();
                _handData.PlayCard(card);
                break;
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
}
