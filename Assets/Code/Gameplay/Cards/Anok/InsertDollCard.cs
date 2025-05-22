using System.Collections.Generic;

internal class InsertDollCard : AnokCard
{
    private int _targetPlace;

    public InsertDollCard() : base()
    {
        _suitNumber = 3;
        ApplySuitText();
    }

    public override void PlayEffect()
    {
        _replaceManager.InsertDoll();
    }
}