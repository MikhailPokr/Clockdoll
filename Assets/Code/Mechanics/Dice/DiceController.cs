using System;
using System.Collections.Generic;
using UnityEngine;

internal class DiceController : IDiceController
{
    private Palette _palette;

    private List<(int sides, int value)> _lastRoll;
    public List<(int sides, int value)> LastRoll => _lastRoll;

    public event Action<List<(int sides, int value)>> DiceRolled;

    public DiceController(Palette palette)
    {
        _palette = palette;
    }

    public List<(int sides, int value)> RollDice(params int[] dice)
    {
        List<(int, int)> results = new();
        for (int i = 0; i < dice.Length; i++)
        {
            results.Add((dice[i], UnityEngine.Random.Range(1, dice[i] + 1)));
        }
        _lastRoll = results;
        DiceRolled?.Invoke(results);
        return results;
    }

    public DiceView GetDice(int sides, int value)
    {
        DiceView dice = sides switch
        {
            4 => _palette.DicePrefabs.D4,
            6 => _palette.DicePrefabs.D6,
            8 => _palette.DicePrefabs.D8,
            10 => _palette.DicePrefabs.D10,
            12 => _palette.DicePrefabs.D12,
            16 => _palette.DicePrefabs.D16,
            20 => _palette.DicePrefabs.D20,
            _ => null,
        };

        dice.Initialize(_palette.DiceNumbers[value - 1]); //нумерация массива с 0, получать кубик удобнее по его реальному значению
        return dice;
    }
}
