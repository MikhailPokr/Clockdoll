using System.Collections.Generic;

internal class InsertDollCard : AnokCard
{
    private int _targetPlace;

    public InsertDollCard() : base()
    {
        ApplyEffectText(3);
    }

    public override void PlayEffect()
    {
        _replaceManager.InsertDoll();
    }
}