internal class ReplaceCard : AnokCard
{
    public ReplaceCard() : base()
    {
        _effect = "Replace 2 dolls";
        _suit = _palette.Numbers[0];
    }

    public override void PlayEffect()
    {
        _replaceManager.Replace();
    }
}