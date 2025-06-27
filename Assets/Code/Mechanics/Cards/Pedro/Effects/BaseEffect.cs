internal abstract class BaseEffect
{
    protected Suit _suit;

    public abstract string StringKey { get; }

    protected BaseEffect(Suit suit)
    {
        _suit = suit;
    }

    public abstract void PlayEffect(out IRequireLock requireLock);
}
