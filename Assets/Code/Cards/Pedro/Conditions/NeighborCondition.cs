using UnityEngine;

internal class NeighborCondition : BaseCondition
{
    private TableData _tableData;
    private bool _checkLeftNeighbor;

    public NeighborCondition()
    {
        _tableData = ServiceLocator.Resolve<TableData>();
        _checkLeftNeighbor = Random.value > 0.5f;
    }

    public override bool Check()
    {
        int currentPos = _tableData.CurrentPlace;
        int neighborPos = _checkLeftNeighbor
            ? (currentPos - 1 + 12) % 12
            : (currentPos + 1) % 12;

        int neighborTruePos = _tableData.GetTrueDollPlace(
            _tableData.GetDollIndex(neighborPos)
        );

        return neighborPos == neighborTruePos;
    }
}