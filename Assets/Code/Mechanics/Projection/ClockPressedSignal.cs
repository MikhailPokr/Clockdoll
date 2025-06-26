internal struct ClockPressedSignal : ISignal
{
    public bool ItsTableClock { get; }

    public ClockPressedSignal(bool isTableClock)
    {
        ItsTableClock = isTableClock;
    }
}