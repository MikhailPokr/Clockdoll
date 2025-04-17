internal class DrawCardCard : AnokCard
{
    private CardSystem _cardSystem;
    private DiceManager _diceManager;

    public DrawCardCard() : base()
    {
        _cardSystem = ServiceLocator.Resolve<CardSystem>();

        _effect = "Draw D4 cards";
        _suit = _palette.Numbers[10];
    }

    public override void PlayEffect()
    {
        var cardsToDraw = _diceManager.RollDice(4, 1);
        _cardSystem.TakeCard(false, cardsToDraw[0].value);
    }
}