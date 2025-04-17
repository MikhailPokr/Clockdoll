internal class DigitCondition : BaseCondition
{
    private DollPlacementController _placementController;

    public DigitCondition()
    {
        _placementController = ServiceLocator.Resolve<DollPlacementController>();
    }

    public override bool Check()
    {
        int truePos = _placementController.GetTrueDollPlace(_placementController.GetCurrentDollIndex());
        return truePos >= 10 && truePos <= 12;
    }
}