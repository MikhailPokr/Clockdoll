internal interface IPlayer : IService
{
    void ReactionState();
    void StartState();
    void RollDiceState();
    void FortuneState();
    void CardChoiceState();
    void CardPlayState();
}
