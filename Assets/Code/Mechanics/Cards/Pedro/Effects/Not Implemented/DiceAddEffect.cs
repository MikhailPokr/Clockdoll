internal class DiceAddEffect : BaseEffect
{
    public override string StringKey => "card_pedro_effect_{0}_dice";

    public DiceAddEffect(Suit suit) : base(suit)
    {
    }

    public override void PlayEffect(out IRequireLock requireLock)
    {
        throw new System.NotImplementedException();
    }
}