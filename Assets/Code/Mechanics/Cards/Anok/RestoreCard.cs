internal class RestoreCard : AnokCard
{
    public override int Number => 9;
    public override string StringKey => "cards_anok_{0}_" + Number;

    public RestoreCard() : base()
    {
    }

    public override void PlayEffect(out IRequireLock requireLock)
    {
        DiceRequireLock diceRequireLock = new DiceRequireLock(false, 16);
        requireLock = new MultipleRequireLock
            (
                diceRequireLock,
                new CashRequireLock(diceRequireLock, true)
            );
    }
}