using System;
using System.Collections.Generic;

internal interface INoteMarkerData : IService
{
    event Action MarkChanged;
    Dictionary<ClockNum, int> GetDollMarkers(ClockNum dollIndex);
    void SetMark(ClockNum num, ClockNum doll);
}