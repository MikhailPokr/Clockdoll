internal class RouletteCondition : BaseCondition
{
    private DiceManager _diceManager;
    private DollPlacementController _placementController;

    public RouletteCondition()
    {
        _diceManager = ServiceLocator.Resolve<DiceManager>();
        _placementController = ServiceLocator.Resolve<DollPlacementController>();
    }

    public override bool Check()
    {
        int pos = _placementController.GetTrueDollPlace(_placementController.GetCurrentDollIndex());
        return _diceManager.LastRoll.Contains((12, pos));
    }
}