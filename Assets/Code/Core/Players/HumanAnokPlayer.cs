using Assets.Code.Logic;
using UnityEngine;
using System.Collections.Generic;

internal class HumanAnokPlayer : IAnokPlayer
{
    private IGameSubStateMachine _gameSubStateMachine;
    private IDiceController _diceController;
    private ICardSystem _cardSystem;
    private IFortuneSystem _fortuneSystem;
    private IDialogueSystem _dialogueSystem;

    private AnokCard _chosenCard;
    private int _rolledValue;

    public HumanAnokPlayer(
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
        if (_gameSubStateMachine.CurrentState == GameSubState.AnokRollDice)
        {
            List<(int sides, int value)> diceList = _diceController.RollDice(12);
            _rolledValue = diceList[0].value;
            return true;
        }
        return false;
    }

    public void OnGameBegin()
    {
        _cardSystem.TakeCard(false, 5);
    }

    public bool OnCardClick(BaseCard card)
    {
        if (_gameSubStateMachine.CurrentState == GameSubState.AnokCardChoice)
        {
            if (!card.CheckCondition())
                return false;

            _chosenCard = card as AnokCard;
            return true;
        }
        return false;
    }


    public void EnterReactionState()
    {
        _gameSubStateMachine.GoToNextState();
    }

    public void EnterStartTurnState()
    {
    }

    public void EnterRollDiceState()
    {
        
    }

    public void EnterFortuneState()
    {
        _fortuneSystem.ApplyReward(_rolledValue);
    }

    public void EnterCardChoiceState()
    {
    }

    public void EnterCardPlayState()
    {
        _cardSystem.PlayCard(_chosenCard);
    }

}
