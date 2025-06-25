using UnityEngine;

internal class NeighborCondition : BaseCondition
{
    private IDollPlacementController _placementController;
    private bool _checkLeftNeighbor;

    public override string StringKey => "card_pedro_condition_{0}_neighbor_" + (_checkLeftNeighbor ? 0 : 1);

    public NeighborCondition()
    {
        _placementController = ServiceLocator.Resolve<IDollPlacementController>();
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