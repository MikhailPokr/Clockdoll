internal struct ViewModeChangedSignal : ISignal
{
    public bool ItsTopView { get; }

    public ViewModeChangedSignal(bool isTopView)
    {
        ItsTopView = isTopView;
    }
}