using System.Collections.Generic;

internal class InsertDollCard : AnokCard
{
    private int _targetPlace;

    public override int Number => 3;
    public override string StringKey => "cards_anok_{0}_" + Number;

    public InsertDollCard() : base()
    {
    }

    public override void PlayEffect()
    {
        _replaceManager.InsertDoll();
    }
}