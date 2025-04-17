using System;
using UnityEngine;

internal class NoteMarkerData : IService
{
    private int[][] _data;

    public Action MarkChanged;

    public int[] GetDollMarkers(int dollIndex) => _data[dollIndex-1];

    public NoteMarkerData()
    {
        _data = new int[12][];
        for (int i = 0; i < 12; i++)
        {
            _data[i] = new int[12];
        }
    }


    public void SetMark(int num, int doll)
    {
        num--; doll--;
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
