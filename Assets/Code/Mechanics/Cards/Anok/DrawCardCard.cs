internal class DrawCardCard : AnokCard
{
    private ICardSystem _cardSystem;
    private IDiceController _diceManager;
    public override int Number => 10;
    public override string StringKey => "cards_anok_{0}_" + Number;

    public DrawCardCard() : base()
    {
        _cardSystem = ServiceLocator.Resolve<ICardSystem>();
        _diceManager = ServiceLocator.Resolve<IDiceController>();
    }


    public override void PlayEffect()
    {
        var cardsToDraw = _diceManager.RollDice(4);
        _cardSystem.TakeCard(false, cardsToDraw[0].value);
    }
}