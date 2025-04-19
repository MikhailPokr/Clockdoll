using System;
using System.Collections.Generic;

internal interface INoteMarkerData : IService
{
    event Action MarkChanged;
    Dictionary<ClockNum, (int MarkType, int Rotation)> GetDollMarkers(ClockNum dollIndex);
    void SetMark(ClockNum num, ClockNum doll);
}