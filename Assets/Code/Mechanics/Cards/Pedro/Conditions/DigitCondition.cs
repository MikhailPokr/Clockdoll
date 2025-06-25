internal class DigitCondition : BaseCondition
{
    private IDollPlacementController _placementController;

    public override string StringKey => "card_pedro_condition_{0}_digit";

    public DigitCondition()
    {
        _placementController = ServiceLocator.Resolve<IDollPlacementController>();
    }

    public override bool Check()
    {
        int truePos = _placementController.GetTrueDollPlace(_placementController.GetCurrentDollIndex());
        return truePos >= 10 && truePos <= 12;
    }
}