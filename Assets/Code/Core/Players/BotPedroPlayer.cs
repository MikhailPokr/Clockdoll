using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.XR;
internal  class BotPedroPlayer : IPedroPlayer, IBotPlayer
{
    private CoreTicker _coreTicker;
    private IGameSubStateMachine _gameSubStateMachine;
    private IDiceController _diceController;
    private ICardSystem _cardSystem;
    private IFortuneSystem _fortuneSystem;

    private PedroCard _chosenCard;
    private int _rolledValue;

    public event System.Action<bool> OnDiceTrayClickRequested;
    public event System.Action<BaseCard> OnCardClickRequested;

    public BotPedroPlayer(
        CoreTicker coreTicker,
        IGameSubStateMachine gameSubStateMachine,
        IDiceController diceController,
        ICardSystem cardSystem,
        IFortuneSystem fortuneSystem
        )
    {
        _coreTicker = coreTicker;
        _gameSubStateMachine = gameSubStateMachine;
        _diceController = diceController;
        _cardSystem = cardSystem;
        _fortuneSystem = fortuneSystem;
    }
    public bool OnTrayClick()
    {
        if (_gameSubStateMachine.CurrentState == GameSubState.PedroRollDice)
        {
            List<(int sides, int value)> diceList = _diceController.RollDice(12);
            _rolledValue = diceList[0].value;
            return true;
        }
        return false;
    }

    public bool OnCardClick(BaseCard card)
    {
        if (_gameSubStateMachine.CurrentState == GameSubState.PedroCardChoice)
        {
            _chosenCard = card as PedroCard;
            return true;
        }
        return false;
    }
    public void OnGameBegin()
    {
        _cardSystem.TakeCard(true, 5);
    }

    public void EnterReactionState()
    {
        _coreTicker.Invoke(() => _gameSubStateMachine.GoToNextState(), 1);
    }

    public void EnterStartTurnState()
    {
    }

    public void EnterRollDiceState()
    {
        _coreTicker.Invoke(() => OnDiceTrayClickRequested?.Invoke(true), 1);
    }

    public void EnterFortuneState()
    {
        _coreTicker.Invoke(() => _fortuneSystem.ApplyReward(_rolledValue), 1);
        
    }

    public void EnterCardChoiceState()
    {
        List<BaseCard> hand = _cardSystem.GetHand(true).Where(x => x.CheckCondition()).ToList();
        if (hand.Count ==0)
        {
            _coreTicker.Invoke(() => OnCardClickRequested.Invoke(null), 1);
            return;
        }
        _coreTicker.Invoke(() => OnCardClickRequested?.Invoke(hand[Random.Range(0, hand.Count)]), 1);
    }

    public void EnterCardPlayState()
    {
        if (_chosenCard != null)
            _cardSystem.PlayCard(_chosenCard);
        _coreTicker.Invoke(() => _gameSubStateMachine.GoToNextState(), 1);
    }
}
