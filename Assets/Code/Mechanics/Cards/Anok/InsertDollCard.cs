using System.Collections.Generic;

internal class InsertDollCard : AnokCard
{
    private int _targetPlace;

    public InsertDollCard() : base()
    {
        _effect = "Insert the doll in the chosen place";
        _suit = _palette.Numbers[3];
    }

    public override void PlayEffect()
    {
        _replaceManager.InsertDoll();
    }
}