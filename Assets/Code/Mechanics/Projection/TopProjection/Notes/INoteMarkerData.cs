using System.Collections.Generic;

internal interface INoteMarkerData : IService
{
    Dictionary<ClockNum, (int MarkType, int Rotation)> GetDollMarkers(ClockNum dollIndex);
    void SetMark(ClockNum num, ClockNum doll);
}