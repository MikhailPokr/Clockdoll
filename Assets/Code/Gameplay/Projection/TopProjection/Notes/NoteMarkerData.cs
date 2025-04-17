using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class NoteMarkerData : INoteMarkerData
{
    private Dictionary<ClockNum, Dictionary<ClockNum, int>> _data;

    public event Action MarkChanged;

    public Dictionary<ClockNum, int> GetDollMarkers(ClockNum dollIndex) => _data[dollIndex];

    public NoteMarkerData()
    {
        _data = new();
        for (int i = ClockNum.MinValue; i <= ClockNum.MaxValue; i++)
        {
            _data[i] = new();
            for(int j = ClockNum.MinValue; j <= ClockNum.MaxValue; j++)
            {
                _data[i][j] = 0;
            }
        }
    }


    public void SetMark(ClockNum num, ClockNum doll)
    {
        if (_data[doll][num] == 3)
            _data[doll][num] = 0;
        else
            _data[doll][num]++;

        MarkChanged?.Invoke();
    }

    public enum Mode
    {
        None = 0,
        Cross = 1,
        Circle = 2,
        Triangele = 3
    }
}
