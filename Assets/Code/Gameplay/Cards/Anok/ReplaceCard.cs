internal class ReplaceCard : AnokCard
{
    public ReplaceCard() : base()
    {
        _suitNumber = 0;
        ApplySuitText();
    }

    public override void PlayEffect()
    {
        _replaceManager.Replace();
    }
}