using Assets.Code.Logic;

internal class DiscardEffect : BaseEffect
{
    public override string StringKey => $"[value:{_value}]" + "card_pedro_effect_{0}_discard";

    private int _value;

    public DiscardEffect(Suit suit) : base(suit)
    {
        _value = _suit switch
        {
            Suit.Diamonds => 3,
            Suit.Spades => 4,
            Suit.Hearts => 1,
            Suit.Crosses => 2,
            _ => 0
        };
    }

    public override void PlayEffect(out IRequireLock requireLock)
    {
        requireLock = new CardRequireLock(false, _value, CardRequireLockType.Discard);
    }
}