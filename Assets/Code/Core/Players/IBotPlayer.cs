using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal interface IBotPlayer
{
    event Action<bool> OnDiceTrayClickRequested;
    event Action<BaseCard> OnCardClickRequested;
}