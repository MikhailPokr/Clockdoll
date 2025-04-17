internal abstract class BaseEffect
{
    protected Suit _suit;

    protected BaseEffect(Suit suit)
    {
        _suit = suit;
    }

    public abstract void PlayEffect();
}
