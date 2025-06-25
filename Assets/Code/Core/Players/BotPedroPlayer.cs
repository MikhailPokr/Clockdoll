using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.XR;
using DG.Tweening;
internal  class BotPedroPlayer : IPedroPlayer, IBotPlayer
{
    private IGameSubStateMachine _gameSubStateMachine;
    private IDiceController _diceController;
    private ICardSystem _cardSystem;
    private IFortuneSystem _fortuneSystem;

    private PedroCard _chosenCard;
    private int _rolledValue;

    public event System.Action<bool> OnDiceTrayClickRequested;
    public event System.Action<BaseCard> OnCardClickRequested;

    public BotPedroPlayer(
        IGameSubStateMachine gameSubStateMachine,
        IDiceController diceController,
        ICardSystem cardSystem,
        IFortuneSystem fortuneSystem
        )
    {
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
        DOVirtual.DelayedCall(1, () => _gameSubStateMachine.GoToNextState());
    }

    public void EnterStartTurnState()
    {
    }

    public void EnterRollDiceState()
    {
        DOVirtual.DelayedCall(1, () => OnDiceTrayClickRequested?.Invoke(true));
    }

    public void EnterFortuneState()
    {
        DOVirtual.DelayedCall(1, () => _fortuneSystem.ApplyReward(_rolledValue));
        
    }

    public void EnterCardChoiceState()
    {
        List<BaseCard> hand = _cardSystem.GetHand(true).Where(x => x.CheckCondition()).ToList();
        if (hand.Count ==0)
        {
            DOVirtual.DelayedCall(1, () => OnCardClickRequested.Invoke(null));
            return;
        }
        DOVirtual.DelayedCall(1, () => OnCardClickRequested?.Invoke(hand[Random.Range(0, hand.Count)]));
    }

    public void EnterCardPlayState()
    {
        _cardSystem.PlayCard(_chosenCard);
    }
}
