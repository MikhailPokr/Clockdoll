internal class PrimeCondition : BaseCondition
{
    private DollPlacementController _placementController;

    public PrimeCondition()
    {
        _placementController = ServiceLocator.Resolve<DollPlacementController>();
    }

    public override bool Check()
    {
        int truePos = _placementController.GetTrueDollPlace(_placementController.GetCurrentDollIndex());
        return truePos == 2 || truePos == 3 || truePos == 5 || truePos == 7 || truePos == 11;
    }
}