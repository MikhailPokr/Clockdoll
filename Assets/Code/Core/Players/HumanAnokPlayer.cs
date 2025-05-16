using Assets.Code.Logic;
using UnityEngine;
using System.Collections.Generic;

internal class HumanAnokPlayer : IAnokPlayer
{
    private int _diceTrayRolledValue;
    private IDialogueSystem _dialogueSystem;
    public HumanAnokPlayer(IDialogueSystem dialogueSystem) {
        _dialogueSystem = dialogueSystem;
     }

    public void SaveDice(List<(int sides, int value)> diceList) {
        _diceTrayRolledValue = diceList[0].value;
        _dialogueSystem.CreateDialogueBoxByKey($"anok_game", _diceTrayRolledValue);
    }

    public void ReactionState()
    {
        //чекать сброс карт
    }

    public void StartState()
    {
        //мб вызвать речь
    }

    public void RollDiceState()
    {
        
    }

    public void FortuneState()
    {
        //реакция на награду
    }

    public void CardChoiceState()
    {
        //чекать нажатие на карту
    }

    public void CardPlayState()
    {
        //разыгровка карты
    }
}
