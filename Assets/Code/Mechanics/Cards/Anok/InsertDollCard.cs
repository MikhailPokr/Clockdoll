internal class InsertDollCard : AnokCard
{
    public override int Number => 3;
    public override string StringKey => "cards_anok_{0}_" + Number;

    public InsertDollCard() : base()
    {
    }

    public override void PlayEffect(out IRequireLock requireLock)
    {
        requireLock = new DollRequireLock(false, 2, DollRequireLockType.Insert);
    }
}