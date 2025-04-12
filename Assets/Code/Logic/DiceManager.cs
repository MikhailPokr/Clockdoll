using System;
using System.Collections.Generic;
using UnityEngine;

internal class DiceManager : IService
{
    private Palette _palette;

    public Action<List<(int sides, int value)>> DiceRolled;
    
    public DiceManager(Palette palette)
    {
        _palette = palette;
    }

    public List<(int sides, int value)> RollDice(params int[] dice)
    {
        List<(int, int)> results = new(); 
        for (int i = 0; i < dice.Length; i++)
        {
            results.Add((dice[i], UnityEngine.Random.Range(1, dice[i])));
        }
        DiceRolled?.Invoke(results);
        return results;
    }

    public DiceModel GetDice(int sides, int value)
    {
        DiceModel dice = sides switch
        {
            4 => GameObject.Instantiate(_palette.DicePrefabs.D4),
            6 => GameObject.Instantiate(_palette.DicePrefabs.D6),
            8 => GameObject.Instantiate(_palette.DicePrefabs.D8),
            10 => GameObject.Instantiate(_palette.DicePrefabs.D10),
            12 => GameObject.Instantiate(_palette.DicePrefabs.D12),
            16 => GameObject.Instantiate(_palette.DicePrefabs.D16),
            20 => GameObject.Instantiate(_palette.DicePrefabs.D20),
            _ => null,
        };

        dice.Initialize(_palette.DiceNumbers[value - 1]);
        return dice;
    }

}
