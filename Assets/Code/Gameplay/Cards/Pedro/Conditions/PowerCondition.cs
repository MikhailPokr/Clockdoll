using UnityEngine;

internal class PowerCondition : BaseCondition
{
    private DollPlacementController _placementController;
    private bool _isPowerOfTwo;

    public PowerCondition()
    {
        _placementController = ServiceLocator.Resolve<DollPlacementController>();
        _isPowerOfTwo = Random.value > 0.5f;
    }

    public override bool Check()
    {
        int truePos = _placementController.GetTrueDollPlace(_placementController.GetCurrentDollIndex());
        if (_isPowerOfTwo)
            return truePos == 1 || truePos == 2 || truePos == 4 || truePos == 8;
        else
            return truePos == 1 || truePos == 3 || truePos == 9;
    }
}