using System.Collections.Generic;

internal interface ICardSystem : IService
{
    event System.Action HandUpdated;
    event System.Action<BaseCard> CardPlayed;
    void AddCard(BaseCard card);
    void DiscardCard(BaseCard card);
    List<BaseCard> GetCurrentHandForView();
    bool IsCardInHand(BaseCard card);
    List<BaseCard> GetHand(bool pedroHand);
    void SwitchHand();
    void TakeCard(bool toPedro, int count, bool spadesGuaranteed = false);
    void PlayCard(BaseCard card);
}