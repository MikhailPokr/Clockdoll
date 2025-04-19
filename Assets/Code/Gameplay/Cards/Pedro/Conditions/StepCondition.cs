using UnityEngine;

internal class StepCondition : BaseCondition
{
    private IDollPlacementController _placementController;
    private int _step;

    public StepCondition()
    {
        _placementController = ServiceLocator.Resolve<IDollPlacementController>();
        _step = Random.Range(1, 5); // от 1 до 4
    }

    public override bool Check()
    {
        int currentPos = _placementController.CurrentPlace;
        int truePos = _placementController.GetTrueDollPlace(_placementController.GetCurrentDollIndex());
        int distance = Mathf.Min(
            (truePos - currentPos + 12) % 12,
            (currentPos - truePos + 12) % 12
        );
        return distance == _step;
    }
}