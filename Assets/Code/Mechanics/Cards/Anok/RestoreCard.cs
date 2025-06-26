internal class RestoreCard : AnokCard
{
    private IAnokCashData _anokCashData;
    private IDiceController _diceManager;

    public override int Number => 9;
    public override string StringKey => "cards_anok_{0}_" + Number;

    public RestoreCard() : base()
    {
        _anokCashData = ServiceLocator.Resolve<IAnokCashData>();
        _diceManager = ServiceLocator.Resolve<IDiceController>();
    }

    public override void PlayEffect()
    {
        var money = _diceManager.RollDice(16);
        _anokCashData.ChangeCash(money[0].value);

    }
}