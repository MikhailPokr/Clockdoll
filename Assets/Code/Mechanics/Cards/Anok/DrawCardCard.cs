internal class DrawCardCard : AnokCard
{
    public override int Number => 10;
    public override string StringKey => "cards_anok_{0}_" + Number;

    public DrawCardCard() : base()
    {
    }


    public override void PlayEffect(out IRequireLock requireLock)
    {
        DiceRequireLock diceRequireLock = new DiceRequireLock(false, 4);
        requireLock = new MultipleRequireLock(
            diceRequireLock,
            new CardRequireLock(false, diceRequireLock, CardRequireLockType.Take)
            );
    }
}