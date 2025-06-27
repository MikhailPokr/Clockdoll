internal class DamageEffect : BaseEffect
{
    private int _dice;

    public override string StringKey => $"[value:{_dice}]"+ "card_pedro_effect_{0}_damage";

    public DamageEffect(Suit suit) : base(suit)
    {
        _dice = _suit switch
        {
            Suit.Diamonds => 16,
            Suit.Spades => 20,
            Suit.Hearts => 10,
            Suit.Crosses => 6,
            _ => 0
        };
    }

    public override void PlayEffect(out IRequireLock requireLock)
    {
        DiceRequireLock diceRequireLock = new DiceRequireLock(true, _dice);
        requireLock = new MultipleRequireLock
            (
            diceRequireLock,
            new CashRequireLock(diceRequireLock, false)
            );
    }
}