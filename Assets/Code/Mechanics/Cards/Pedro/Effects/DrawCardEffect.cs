internal class DrawCardEffect : BaseEffect
{
    private ICardSystem _cardSystem;

    public override string StringKey => "card_pedro_effect_{0}_draw";

    public DrawCardEffect(Suit suit) : base(suit)
    {
        _cardSystem = ServiceLocator.Resolve<ICardSystem>();
    }

    public override void PlayEffect()
    {

    }
}