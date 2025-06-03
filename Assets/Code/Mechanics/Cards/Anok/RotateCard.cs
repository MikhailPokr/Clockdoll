using System.Collections.Generic;
using UnityEngine;

internal class RotateCard : AnokCard
{
    private bool _isClockwise;

    public RotateCard() : base()
    {
        _isClockwise = Random.value > 0.5f;

        suitNumber = 1;
        _effect = _dialogueSystem.ReturnJsonData("cards_anok", suitNumber).content
        + _dialogueSystem.ReturnJsonData("cards_anok", suitNumber).variations
        [_isClockwise ? 1 : 0];
        _suit = _palette.Numbers[suitNumber];
    }

    public override void PlayEffect()
    {
        _replaceManager.RotateAll(_isClockwise);
    }
}