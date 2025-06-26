internal struct CardClickRequestedSignal : ISignal
{
    public bool ItsPedro { get; }
    public BaseCard Card { get; }

    public CardClickRequestedSignal(bool isPedro, BaseCard card)
    {
        ItsPedro = isPedro;
        Card = card;
    }
}