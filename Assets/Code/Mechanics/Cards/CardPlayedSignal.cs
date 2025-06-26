internal struct CardPlayedSignal : ISignal
{
    public BaseCard Card { get; }

    public CardPlayedSignal(BaseCard card)
    {
        Card = card;
    }
}