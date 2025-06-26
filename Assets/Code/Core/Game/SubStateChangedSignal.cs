internal struct SubStateChangedSignal : ISignal
{
    public GameSubState GameSubState { get; }
    public ClockNum CurrentPlace { get; }

    public SubStateChangedSignal(GameSubState gameSubState, ClockNum currentPlace)
    {
        GameSubState = gameSubState;
        CurrentPlace = currentPlace;
    }
}