using System;
using System.Collections.Generic;

internal class DiceManager : IService
{
    public Action<List<(int sides, int value)>> DiceRolled;

    public List<(int sides, int value)> RollTheDice(params int[] dice)
    {
        List<(int, int)> results = new(); 
        for (int i = 0; i < dice.Length; i++)
        {
            results.Add((dice[i], UnityEngine.Random.Range(1, dice[i])));
        }
        DiceRolled?.Invoke(results);
        return results;
    }

}
