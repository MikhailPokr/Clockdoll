using UnityEngine;

internal class SideCondition : BaseCondition
{
    private TableData _tableData;
    private bool _isRightSide;

    public SideCondition()
    {
        _tableData = ServiceLocator.Resolve<TableData>();
        _isRightSide = Random.value > 0.5f;
    }

    public override bool Check()
    {
        int truePos = _tableData.GetTrueDollPlace(_tableData.GetCurrentDollIndex());
        return _isRightSide
            ? truePos >= 7 && truePos <= 11
            : truePos >= 1 && truePos <= 5;
    }
}