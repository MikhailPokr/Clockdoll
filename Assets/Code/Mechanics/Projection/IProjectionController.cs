using System;

internal interface IProjectionController : IService
{
    event Action<bool> ViewModeChanged;

    void ChangeView(bool isTableClock);
    void OnClockClick(bool isTableClock);
}