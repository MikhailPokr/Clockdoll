using System.Linq;

internal class CashRequireLock : IRequireLock
{
    public int[] Dice { get; }
    public bool ItsDone { get; private set; }

    private int _cashValue;
    private bool _isIncrease;
    public bool IsIncrease => _isIncrease;

    DiceRequireLock _dice;
    public int CashValue
    { 
        get
        {
            if (_dice != null)
            {
                if (_dice.DiceResult == null)
                    return -1;
                else
                    return _dice.DiceResult.Sum();
            }
            return _cashValue;

        }
    }

    public CashRequireLock(int deltaCash)
    {
        if (deltaCash < 0)
        {
            _cashValue = -deltaCash;
            _isIncrease = false;
        }
        else
        {
            _cashValue = deltaCash;
            _isIncrease = true;
        }
        ItsDone = false;
    }

    public CashRequireLock(DiceRequireLock diceRequireLock, bool isIncrease)
    {
        _dice = diceRequireLock;
        _isIncrease = isIncrease;
        ItsDone = false;
    }

    public void СonfirmСompletion()
    {
        ItsDone = true;
    }

}