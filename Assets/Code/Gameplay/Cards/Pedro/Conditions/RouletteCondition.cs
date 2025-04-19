internal class RouletteCondition : BaseCondition
{
    private IDollPlacementController _placementController;
    private IDiceController _diceManager;

    public RouletteCondition()
    {
        _placementController = ServiceLocator.Resolve<IDollPlacementController>();
        _diceManager = ServiceLocator.Resolve<IDiceController>();
    }

    public override bool Check()
    {
        int pos = _placementController.GetTrueDollPlace(_placementController.GetCurrentDollIndex());
        return _diceManager.LastRoll.Contains((12, pos));
    }
}