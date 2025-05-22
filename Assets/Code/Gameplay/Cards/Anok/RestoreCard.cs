using UnityEngine;

internal class RestoreCard : AnokCard
{
    private IAnokCashData _anokCashData;
    private IDiceController _diceManager;

    public RestoreCard() : base()
    {
        _anokCashData = ServiceLocator.Resolve<IAnokCashData>();
        _diceManager = ServiceLocator.Resolve<IDiceController>();

        _suitNumber = 9;
        ApplySuitText();
    }

    public override void PlayEffect()
    {
        var money = _diceManager.RollDice(16, 1);
        _anokCashData.ChangeCash(money[0].value);
        
    }
}