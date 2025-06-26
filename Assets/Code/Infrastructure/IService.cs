internal interface IService
{
    void Dispose()
    {
        SignalBus.UnsubscribeAll(this);
    }
}