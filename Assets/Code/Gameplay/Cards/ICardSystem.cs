using System.Collections.Generic;

internal interface ICardSystem : IService
{
    void AddCard(BaseCard card);
    void DiscardCard(BaseCard card);
    List<BaseCard> GetCurrentHand();
    List<BaseCard> GetHand(bool pedroHand);
    void SwitchHand();
    void TakeCard(bool toPedro, int count, bool spadesGuaranteed = false);
    bool TryPlayCard(BaseCard card);
}