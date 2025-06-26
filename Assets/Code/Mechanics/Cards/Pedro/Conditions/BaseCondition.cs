internal abstract class BaseCondition
{
    protected Suit _suit;
    public Suit Suit => _suit;
    public abstract string StringKey { get; }

    public abstract bool Check();

}
