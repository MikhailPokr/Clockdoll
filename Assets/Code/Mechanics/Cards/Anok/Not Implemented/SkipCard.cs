internal class SkipCard : AnokCard
{
    public override string StringKey => throw new System.NotImplementedException();
    public override int Number => throw new System.NotImplementedException();
    public SkipCard() : base()
    {
    }

    public override void PlayEffect(out IRequireLock requireLock)
    {
        throw new System.NotImplementedException();
    }
}