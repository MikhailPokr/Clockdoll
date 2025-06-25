using UnityEngine;

internal class CloserCondition : BaseCondition
{
    private IDollPlacementController _placementController;
    private bool _isClockwise;
    public override string StringKey => "card_pedro_condition_{0}_closer_" + (_isClockwise ? 0 : 1);
    public CloserCondition()
    {
        _placementController = ServiceLocator.Resolve<IDollPlacementController>();
        _isClockwise = Random.value > 0.5f;
    }


    public override bool Check()
    {
        int currentPos = _placementController.CurrentPlace;
        int truePos = _placementController.GetTrueDollPlace(_placementController.GetCurrentDollIndex());

        int distanceClockwise = (truePos - currentPos + 12) % 12;
        int distanceCounter = (currentPos - truePos + 12) % 12;

        bool isCloser = _isClockwise
            ? distanceClockwise < distanceCounter
            : distanceCounter < distanceClockwise;

        return isCloser;
    }
}