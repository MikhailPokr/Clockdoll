internal class DiceRequireLock : IRequireLock
{
    public int[] Dice { get; }
    public bool ItsDone { get; private set; }
    public bool ItsForPedro { get; }

    public int[] DiceResult { get; private set; }

    public DiceRequireLock(bool forPedro, params int[] dice)
    {
        ItsForPedro = forPedro;
        ItsDone = false;
        Dice = dice;
    }

    public void СonfirmСompletion(params int[] diceResult)
    {
        ItsDone = true;
        DiceResult = diceResult;
    }

}