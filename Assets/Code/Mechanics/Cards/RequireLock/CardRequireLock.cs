using System;
using System.Collections.Generic;
using System.Linq;

internal class CardRequireLock : IRequireLock
{
    public bool ItsForPedro { get; }
    public CardRequireLockType LockType { get; }

    private int _count;
    private DiceRequireLock _dice;

    private List<BaseCard> _selectedObjects;

    public bool ItsDone
    {
        get
        {
            if (_dice != null)
            {
                if (_dice.DiceResult == null)
                    return false;
                return _selectedObjects.Count >= _dice.DiceResult.Sum();
            }
            return _selectedObjects.Count >= _count;
        }
    }


    public CardRequireLock(bool forPredro, int count, CardRequireLockType lockType)
    {
        ItsForPedro = forPredro;
        _count = count;
        LockType = lockType;

        _selectedObjects = new List<BaseCard>();
    }
    public CardRequireLock(bool forPredro, DiceRequireLock dice, CardRequireLockType lockType)
    {
        ItsForPedro = forPredro;
        _dice = dice;
        LockType = lockType;

        _selectedObjects = new List<BaseCard>();
    }


    public void Add(BaseCard selectedCard)
    {
        if (selectedCard is PedroCard == ItsForPedro)
            _selectedObjects.Add(selectedCard);
    }
}
