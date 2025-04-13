internal class RouletteCondition : BaseCondition
{
    private DiceManager _diceManager;
    private TableData _tableData;

    public RouletteCondition()
    {
        _diceManager = ServiceLocator.Resolve<DiceManager>();
        _tableData = ServiceLocator.Resolve<TableData>();
    }

    public override bool Check()
    {
        int pos = _tableData.GetTrueDollPlace(_tableData.GetCurrentDollIndex());
        return _diceManager.LastRoll.Contains((12, pos));
    }
}