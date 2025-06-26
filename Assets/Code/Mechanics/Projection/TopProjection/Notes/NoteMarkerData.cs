using System.Collections.Generic;
using UnityEngine;

internal class NoteMarkerData : INoteMarkerData
{
    private Dictionary<ClockNum, Dictionary<ClockNum, (int MarkType, int Rotation)>> _data;

    public Dictionary<ClockNum, (int, int)> GetDollMarkers(ClockNum dollIndex) => _data[dollIndex];

    public NoteMarkerData()
    {
        _data = new();
        for (int i = ClockNum.MinValue; i <= ClockNum.MaxValue; i++)
        {
            _data[i] = new();
            for (int j = ClockNum.MinValue; j <= ClockNum.MaxValue; j++)
            {
                _data[i][j] = (0, 0);
            }
        }
    }
    public void SetMark(ClockNum num, ClockNum doll)
    {
        int mode;
        if (_data[doll][num].MarkType == 3)
            mode = 0;
        else
            mode = _data[doll][num].MarkType + 1;

        _data[doll][num] = (mode, Random.Range(0, 360));
        SignalBus.Publish(new MarkChangedSignal());
    }

    public enum Mode
    {
        None = 0,
        Cross = 1,
        Circle = 2,
        Triangle = 3
    }
}
