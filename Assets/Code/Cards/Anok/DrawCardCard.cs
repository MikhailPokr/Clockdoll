internal class DrawCardCard : AnokCard
{
    private HandData _handData;
    private DiceManager _diceManager;

    public DrawCardCard() : base()
    {
        _handData = ServiceLocator.Resolve<HandData>();

        _effect = "Draw D4 cards";
        _suit = _palette.Numbers[10];
    }

    public override void PlayEffect()
    {
        var cardsToDraw = _diceManager.RollDice(4, 1);
        _handData.TakeCard(false, cardsToDraw[0].value);
    }
}