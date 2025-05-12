using Assets.Code.Logic;
using UnityEngine;

internal class HumanAnokPlayer : IAnokPlayer
{
    public HumanAnokPlayer() { }

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
        //чекать нажатие на дайсдрей
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
