internal class ReplaceCard : AnokCard
{
    public ReplaceCard() : base()
    {
        ApplyEffectText(0);
    }

    public override void PlayEffect()
    {
        _replaceManager.Replace();
    }
}