using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class NoteView : MonoBehaviour
{
    [SerializeField] private Image[] _markPlace;
    [SerializeField] private Image _symbol;
    [SerializeField] private Color _markColor;

    private Dictionary<ClockNum, Image> _marks;
    private Palette.Markers _markers;
    private NoteGroup _noteGroup;

    public void Initialize(Palette.Markers markers, NoteGroup noteGroup)
    {
        _markers = markers;
        _noteGroup = noteGroup;

        _marks = new Dictionary<ClockNum, Image>();
        for (int i = ClockNum.MinValue; i <= ClockNum.MaxValue; i++)
        {
            _marks[i] = _markPlace[i - 1]; //индексация массива с 0, словарь с 1
        }
    }
    public void Click(int num)
    {
        _noteGroup.Click(num);
    }

    public void UpdateMark(Dictionary<ClockNum, (int MarkType, int Rotation)> mode, Sprite symbol)
    {
        _symbol.sprite = symbol;
        for (int i = ClockNum.MinValue; i <= ClockNum.MaxValue; i++)
        {
            if (mode[i].MarkType == 0)
            {
                _marks[i].color = new(0, 0, 0, 0);
                continue;
            }
            _marks[i].color = _markColor;

            _marks[i].sprite = mode[i].MarkType switch
            {
                1 => _markers.Cross,
                2 => _markers.Circle,
                3 => _markers.Triangle,
                _ => null
            };
            _marks[i].transform.eulerAngles = new(0, 0, mode[i].Rotation);
        }
    }
}
