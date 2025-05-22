internal class DrawCardCard : AnokCard
{
    private ICardSystem _cardSystem;
    private IDiceController _diceManager;

    public DrawCardCard() : base()
    {
        _cardSystem = ServiceLocator.Resolve<ICardSystem>();
        _diceManager = ServiceLocator.Resolve<IDiceController>();

        _suitNumber = 10;
        ApplySuitText();
    }

    public override void PlayEffect()
    {
        var cardsToDraw = _diceManager.RollDice(4, 1);
        _cardSystem.TakeCard(false, cardsToDraw[0].value);
    }
}