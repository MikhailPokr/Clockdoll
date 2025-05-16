using System.Collections.Generic;

internal interface IPlayer : IService
{
    void ReactionState();
    void StartState();
    void RollDiceState();
    void FortuneState();
    void CardChoiceState();
    void CardPlayState();
    void SaveDice(List<(int sides, int value)> diceList);
}
