internal interface IProjectionController : IService
{
    void ChangeView(bool isTableClock);
}