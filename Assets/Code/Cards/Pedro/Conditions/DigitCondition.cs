internal class DigitCondition : BaseCondition
{
    private TableData _tableData;

    public DigitCondition()
    {
        _tableData = ServiceLocator.Resolve<TableData>();
    }

    public override bool Check()
    {
        int truePos = _tableData.GetTrueDollPlace(_tableData.GetCurrentDollIndex());
        return truePos >= 10 && truePos <= 12;
    }
}