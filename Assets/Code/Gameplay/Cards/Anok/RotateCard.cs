using System.Collections.Generic;
using UnityEngine;

internal class RotateCard : AnokCard
{
    private bool _isClockwise;

    public RotateCard() : base()
    {
        _isClockwise = Random.value > 0.5f;

        _effect = $"Replace everyone {(_isClockwise ? "" : "counter")}clockwise";
        _suit = _palette.Numbers[1];
    }

    public override void PlayEffect()
    {
        _replaceManager.RotateAll(_isClockwise);
    }
}