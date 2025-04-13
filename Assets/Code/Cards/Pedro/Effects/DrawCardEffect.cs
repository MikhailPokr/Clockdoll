using Assets.Code.Logic;

internal class DrawCardEffect : BaseEffect
{
    private HandData _handData;

    public DrawCardEffect(Suit suit) : base(suit)
    {
        _handData = ServiceLocator.Resolve<HandData>();
    }

    public override void PlayEffect()
    {
        
    }
}