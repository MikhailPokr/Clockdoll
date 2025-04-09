using System.Collections.Generic;
using UnityEngine;

internal class CardManager : IService
{
    private List<BaseCard> _pedroLibrary = new();
    private List<BaseCard> _anokLibrary = new();

    private List<BaseCard> _pedroHand = new();
    private List<BaseCard> _anokHand = new();


    public void TakeCard(bool toPedro, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (toPedro)
                _pedroHand.Add(GenerateDollCard());
            else
                _anokHand.Add(GenerateGuestCard());
        }
    }

    public bool PlayCard()
    {
        //поверить условие и при нессотвествии выдать false
        throw new System.NotImplementedException();
    }

    private BaseCard GenerateDollCard()
    {
        throw new System.NotImplementedException();
    }
    private BaseCard GenerateGuestCard()
    {
        throw new System.NotImplementedException();
    }
}
