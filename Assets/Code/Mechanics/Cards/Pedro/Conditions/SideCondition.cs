using UnityEngine;

internal class SideCondition : BaseCondition
{
    private IDollPlacementController _placementController;
    private bool _isRightSide;

    public override string StringKey => "card_pedro_condition_{0}_side_" + (_isRightSide ? 0 : 1);

    public SideCondition()
    {
        _placementController = ServiceLocator.Resolve<IDollPlacementController>();
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