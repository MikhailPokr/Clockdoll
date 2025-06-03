using UnityEngine;

internal class RestoreCard : AnokCard
{
    private IAnokCashData _anokCashData;
    private IDiceController _diceManager;

    public RestoreCard() : base()
    {
        _anokCashData = ServiceLocator.Resolve<IAnokCashData>();
        _diceManager = ServiceLocator.Resolve<IDiceController>();

        ApplyEffectText(9);
    }

    public override void PlayEffect()
    {
        var money = _diceManager.RollDice(16);
        _anokCashData.ChangeCash(money[0].value);
        
    }
}