internal class PrimeCondition : BaseCondition
{
    private IDollPlacementController _placementController;

    public override string StringKey => "card_pedro_condition_{0}_prime";

    public PrimeCondition()
    {
        _placementController = ServiceLocator.Resolve<IDollPlacementController>();
    }

    public override bool Check()
    {
        int truePos = _placementController.GetTrueDollPlace(_placementController.GetCurrentDollIndex());
        return truePos == 2 || truePos == 3 || truePos == 5 || truePos == 7 || truePos == 11;
    }
}