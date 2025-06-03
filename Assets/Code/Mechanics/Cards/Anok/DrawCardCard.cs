internal class DrawCardCard : AnokCard
{
    private ICardSystem _cardSystem;
    private IDiceController _diceManager;

    public DrawCardCard() : base()
    {
        _cardSystem = ServiceLocator.Resolve<ICardSystem>();
        _diceManager = ServiceLocator.Resolve<IDiceController>();

        ApplyEffectText(10);
    }

    public override void PlayEffect()
    {
        var cardsToDraw = _diceManager.RollDice(4);
        _cardSystem.TakeCard(false, cardsToDraw[0].value);
    }
}