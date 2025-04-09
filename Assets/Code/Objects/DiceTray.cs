using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

internal class DiceTray : MonoBehaviour, IInitializable
{
    [SerializeField] private TextMeshProUGUI _text;

    private DiceManager _diceManager;
    public void Initialize()
    {
        _diceManager = ServiceLocator.Resolve<DiceManager>();
        _diceManager.DiceRolled += OnDiceRolled;
    }

    private void OnDiceRolled(List<(int sides, int value)> list)
    {
        string text = "";
        for (int i  = 0; i < list.Count; i++)
        {
            text += $"D{list[i].sides}: {list[i].value};";
        }
        _text.text = text;
    }
}
