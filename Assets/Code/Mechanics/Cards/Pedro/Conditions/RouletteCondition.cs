internal class RouletteCondition : BaseCondition
{
    private IDollPlacementController _placementController;
    private IDiceController _diceManager;

    public override string StringKey => "card_pedro_condition_{0}_roulette";

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