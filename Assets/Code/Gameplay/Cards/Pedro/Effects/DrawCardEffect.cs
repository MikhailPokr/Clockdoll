using Assets.Code.Logic;

internal class DrawCardEffect : BaseEffect
{
    private CardSystem _cardSystem;

    public DrawCardEffect(Suit suit) : base(suit)
    {
        _cardSystem = ServiceLocator.Resolve<CardSystem>();
    }

    public override void PlayEffect()
    {
        
    }
}