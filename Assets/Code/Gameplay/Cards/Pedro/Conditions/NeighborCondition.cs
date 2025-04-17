using UnityEngine;

internal class NeighborCondition : BaseCondition
{
    private DollPlacementController _placementController;
    private bool _checkLeftNeighbor;

    public NeighborCondition()
    {
        _placementController = ServiceLocator.Resolve<DollPlacementController>();
        _checkLeftNeighbor = Random.value > 0.5f;
    }

    public override bool Check()
    {
        int currentPos = _placementController.CurrentPlace;
        int neighborPos = _checkLeftNeighbor
            ? (currentPos - 1 + 12) % 12
            : (currentPos + 1) % 12;

        int neighborTruePos = _placementController.GetTrueDollPlace(
            _placementController.GetDollIndex(neighborPos)
        );

        return neighborPos == neighborTruePos;
    }
}