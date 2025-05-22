using System.Collections.Generic;
using UnityEngine;

internal class RotateCard : AnokCard
{
    private bool _isClockwise;

    public RotateCard() : base()
    {
        _isClockwise = Random.value > 0.5f;

        //_effect = $"Replace everyone {(_isClockwise ? "" : "counter")}clockwise";

        _suitNumber = 1;
        _effect = _dialogueSystem.ReturnJsonData("cards_anok", _suitNumber).content
        + _dialogueSystem.ReturnJsonData("cards_anok", _suitNumber).variations
        [_isClockwise ? 1 : 0];
        _suit = _palette.Numbers[1];
    }

    public override void PlayEffect()
    {
        _replaceManager.RotateAll(_isClockwise);
    }
}