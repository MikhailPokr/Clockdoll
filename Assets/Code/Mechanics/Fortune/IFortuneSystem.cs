using System.Collections.Generic;

internal interface IFortuneSystem : IService
{
    Dictionary<int, Reward> CurrentList { get; }
    void ApplyReward(int number);
    void GenerateNewList();
    int DiceEdges { get; }
}