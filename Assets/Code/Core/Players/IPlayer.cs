using System.Collections.Generic;

internal interface IPlayer : IService
{
    void OnGameBegin();
    void EnterReactionState();
    void EnterStartTurnState();
    void EnterRollDiceState();
    void EnterFortuneState();
    void EnterCardChoiceState();
    void EnterCardPlayState();
    bool OnTrayClick();
    bool OnCardClick(BaseCard card);
}
