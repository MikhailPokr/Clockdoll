using UnityEngine;

internal class RestoreCard : AnokCard
{
    private AnokCashData _anokCashData;
    private DiceManager _diceManager;

    public RestoreCard() : base()
    {
        _anokCashData = ServiceLocator.Resolve<AnokCashData>();

        _effect = "Get D16 money";
        _suit = _palette.Numbers[9];
    }

    public override void PlayEffect()
    {
        var money = _diceManager.RollDice(16, 1);
        _anokCashData.ChangeCash(money[0].value);
        
    }
}