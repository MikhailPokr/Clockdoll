using Assets.Code.Logic;

internal class DiscardEffect : BaseEffect
{
    private IDiscardManager _discardManager;

    public DiscardEffect(Suit suit) : base(suit)
    {
        _discardManager = ServiceLocator.Resolve<IDiscardManager>();
    }

    public override void PlayEffect()
    {
        int count = _suit switch
        {
            Suit.Diamonds => 3,
            Suit.Spades => 4,
            Suit.Hearts => 2,
            Suit.Crosses => 1,
            _ => 0
        };
        _discardManager.AddDiscard(false, count);
    }
}