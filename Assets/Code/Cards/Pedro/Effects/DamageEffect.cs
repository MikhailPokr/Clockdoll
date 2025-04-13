internal class DamageEffect : BaseEffect
{
    private AnokCashData _anokCashData;
    private DiceManager _diceManager;

    public DamageEffect(Suit suit) : base(suit)
    {
        _anokCashData = ServiceLocator.Resolve<AnokCashData>();
        _diceManager = ServiceLocator.Resolve<DiceManager>();
    }

    public override void PlayEffect()
    {
        int side = _suit switch
        {
            Suit.Diamonds => 16,
            Suit.Spades => 20,
            Suit.Hearts => 10,
            Suit.Crosses => 6
        };
        var damage = _diceManager.RollDice(side);
        _anokCashData.ChangeCash(-damage[0].value);
    }
}