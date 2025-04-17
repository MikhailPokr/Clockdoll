using UnityEngine;

internal class SideCondition : BaseCondition
{
    private DollPlacementController _placementController;
    private bool _isRightSide;

    public SideCondition()
    {
        _placementController = ServiceLocator.Resolve<DollPlacementController>();
        _isRightSide = Random.value > 0.5f;
    }

    public override bool Check()
    {
        int truePos = _placementController.GetTrueDollPlace(_placementController.GetCurrentDollIndex());
        return _isRightSide
            ? truePos >= 7 && truePos <= 11
            : truePos >= 1 && truePos <= 5;
    }
}