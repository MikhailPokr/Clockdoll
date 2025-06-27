internal class DrawCardEffect : BaseEffect
{
    private int _value;

    public override string StringKey => $"[value:{_value}]" + "card_pedro_effect_{0}_draw";

    public DrawCardEffect(Suit suit) : base(suit)
    {
        _value = _suit switch
        {
            Suit.Diamonds => 3,
            Suit.Spades => 5,
            Suit.Hearts => 1,
            Suit.Crosses => 2,
            _ => 0
        };

    }

    public override void PlayEffect(out IRequireLock requireLock)
    {
        requireLock = new CardRequireLock(true, _value, CardRequireLockType.Take);
    }
}