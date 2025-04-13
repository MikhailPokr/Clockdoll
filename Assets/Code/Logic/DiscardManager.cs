using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Logic
{
    internal class DiscardManager : IService
    {
        private int _pedroDiscard;
        private int _anokDiscard;

        private HandData _handData;

        public DiscardManager(HandData handData)
        {
            _handData = handData;
        }

        public bool NeedDiscard(bool pedro) => pedro ? _pedroDiscard != 0 : _anokDiscard != 0;
        public void AddDiscard(bool forPedro, int count)
        {
            if (forPedro)
                _pedroDiscard  = count;
            else
                _anokDiscard = count;
        }

        public bool Discard(BaseCard card)
        {
            if (card is AnokCard)
            {
                if (_anokDiscard < 0)
                    return false;
                _handData.DiscardCard(card);
                _anokDiscard--;
                return true;
            }
            else
            {
                if (_pedroDiscard < 0)
                    return false;
                _handData.DiscardCard(card);
                _pedroDiscard--;
                return true;
            }
        }
    }
}
