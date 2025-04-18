using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class NoteView : MonoBehaviour
{
    [SerializeField] private Image[] _markPlace;
    [SerializeField] private Color _markColor;

    private Palette.Markers _markers;
    private NoteGroup _noteGroup;

    public void Initialize(Palette.Markers markers, NoteGroup noteGroup)
    {
        _markers = markers;
        _noteGroup = noteGroup;
    }
    public void Click(int num)
    {
        _noteGroup.Click(num);
    }

    public void UpdateMark(Dictionary<ClockNum, int> mode)
    {
        for (int i = 0; i < _markPlace.Length; i++)
        {
            if (mode[i] == 0)
            {
                _markPlace[i].color = new(0, 0, 0, 0);
                continue;
            }
            _markPlace[i].color = _markColor;

            _markPlace[i].sprite = mode[i] switch
            {
                1 => _markers.Cross,
                2 => _markers.Circle,
                3 => _markers.Triangle,
                _ => null
            };

        }
    }
}
