internal class DamageEffect : BaseEffect
{
    private IAnokCashData _anokCashData;
    private IDiceController _diceManager;

    public DamageEffect(Suit suit) : base(suit)
    {
        _anokCashData = ServiceLocator.Resolve<IAnokCashData>();
        _diceManager = ServiceLocator.Resolve<IDiceController>();
    }

    public override void PlayEffect()
    {
        int side = _suit switch
        {
            Suit.Diamonds => 16,
            Suit.Spades => 20,
            Suit.Hearts => 10,
            Suit.Crosses => 6,
            _ => 0
        };
        var damage = _diceManager.RollDice(side);
        _anokCashData.ChangeCash(-damage[0].value);
    }
}