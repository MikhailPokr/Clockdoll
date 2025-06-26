internal struct CurrentPlaceChangedSignal : ISignal
{
    public ClockNum CurrentPlace { get; }

    public CurrentPlaceChangedSignal(ClockNum currentPlace)
    {
        CurrentPlace = currentPlace;
    }
}