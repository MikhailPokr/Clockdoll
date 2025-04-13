using UnityEngine;

internal class StepCondition : BaseCondition
{
    private TableData _tableData;
    private int _step;

    public StepCondition()
    {
        _tableData = ServiceLocator.Resolve<TableData>();
        _step = Random.Range(1, 5); // от 1 до 4
    }

    public override bool Check()
    {
        int currentPos = _tableData.CurrentPlace;
        int truePos = _tableData.GetTrueDollPlace(_tableData.GetCurrentDollIndex());
        int distance = Mathf.Min(
            (truePos - currentPos + 12) % 12,
            (currentPos - truePos + 12) % 12
        );
        return distance == _step;
    }
}