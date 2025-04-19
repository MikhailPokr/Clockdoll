using Assets.Code.Logic;

internal class DrawCardEffect : BaseEffect
{
    private ICardSystem _cardSystem;

    public DrawCardEffect(Suit suit) : base(suit)
    {
        _cardSystem = ServiceLocator.Resolve<ICardSystem>();
    }

    public override void PlayEffect()
    {
        
    }
}