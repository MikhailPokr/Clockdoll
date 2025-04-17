using System;
using System.Collections.Generic;

internal interface IDiceController : IService
{
    List<(int sides, int value)> LastRoll { get; }

    event Action<List<(int sides, int value)>> DiceRolled;
    DiceView GetDice(int sides, int value);
    List<(int sides, int value)> RollDice(params int[] dice);

}