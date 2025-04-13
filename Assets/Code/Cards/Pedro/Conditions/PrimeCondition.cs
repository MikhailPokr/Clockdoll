internal class PrimeCondition : BaseCondition
{
    private TableData _tableData;

    public PrimeCondition()
    {
        _tableData = ServiceLocator.Resolve<TableData>();
    }

    public override bool Check()
    {
        int truePos = _tableData.GetTrueDollPlace(_tableData.GetCurrentDollIndex());
        return truePos == 2 || truePos == 3 || truePos == 5 || truePos == 7 || truePos == 11;
    }
}