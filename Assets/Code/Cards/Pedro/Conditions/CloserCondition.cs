using UnityEngine;

internal class CloserCondition : BaseCondition
{
    private TableData _tableData;
    private bool _isClockwise;
    public CloserCondition()
    {
        _tableData = ServiceLocator.Resolve<TableData>();
        _isClockwise = Random.value > 0.5f;
    }

    public override bool Check()
    {
        int currentPos = _tableData.CurrentPlace;
        int truePos = _tableData.GetTrueDollPlace(_tableData.GetCurrentDollIndex());

        int distanceClockwise = (truePos - currentPos + 12) % 12;
        int distanceCounter = (currentPos - truePos + 12) % 12;

        bool isCloser = _isClockwise
            ? distanceClockwise < distanceCounter
            : distanceCounter < distanceClockwise;

        return isCloser;
    }
}