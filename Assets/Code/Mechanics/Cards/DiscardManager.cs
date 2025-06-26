namespace Assets.Code.Logic
{
    internal class DiscardManager : IDiscardManager
    {
        private int _pedroDiscard;
        private int _anokDiscard;

        private ICardSystem _cardSystem;

        public DiscardManager(ICardSystem cardSystem)
        {
            _cardSystem = cardSystem;
        }

        public bool NeedDiscard(bool pedro) => pedro ? _pedroDiscard != 0 : _anokDiscard != 0;
        public void AddDiscard(bool forPedro, int count)
        {
            if (forPedro)
                _pedroDiscard = count;
            else
                _anokDiscard = count;
        }

        public bool Discard(BaseCard card)
        {
            if (card is AnokCard)
            {
                if (_anokDiscard < 0)
                    return false;
                _cardSystem.DiscardCard(card);
                _anokDiscard--;
                return true;
            }
            else
            {
                if (_pedroDiscard < 0)
                    return false;
                _cardSystem.DiscardCard(card);
                _pedroDiscard--;
                return true;
            }
        }
    }
}
