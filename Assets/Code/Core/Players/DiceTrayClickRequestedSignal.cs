internal struct DiceTrayClickRequestedSignal : ISignal
{
    public bool ItsPedro { get; }

    public DiceTrayClickRequestedSignal(bool isPedro)
    {
        ItsPedro = isPedro;
    }
}