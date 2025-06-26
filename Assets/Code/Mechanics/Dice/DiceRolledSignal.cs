using System.Collections.Generic;

internal struct DiceRolledSignal : ISignal
{
    public List<(int, int)> Results { get; }

    public DiceRolledSignal(List<(int, int)> results)
    {
        Results = results;
    }
}