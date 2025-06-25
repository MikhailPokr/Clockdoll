internal class ReplaceCard : AnokCard
{
    public override int Number => 0;
    public override string StringKey => "cards_anok_{0}_" + Number;
    public ReplaceCard() : base()
    {
    }

    public override void PlayEffect()
    {
        _replaceManager.Replace();
    }
}