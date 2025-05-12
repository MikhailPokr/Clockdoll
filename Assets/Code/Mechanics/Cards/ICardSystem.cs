using System.Collections.Generic;

internal interface ICardSystem : IService
{
    event System.Action HandUpdated;
    void AddCard(BaseCard card);
    void DiscardCard(BaseCard card);
    List<BaseCard> GetCurrentHandForView();
    List<BaseCard> GetHand(bool pedroHand);
    void SwitchHand();
    void TakeCard(bool toPedro, int count, bool spadesGuaranteed = false);
    bool TryPlayCard(BaseCard card);
}