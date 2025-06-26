using System.Collections.Generic;
using System;

internal interface IFortuneSystem : IService
{
    Dictionary<int, Reward> CurrentList { get; }
    void ApplyReward(int number);
    void GenerateNewList();
    int DiceEdges { get; }
}