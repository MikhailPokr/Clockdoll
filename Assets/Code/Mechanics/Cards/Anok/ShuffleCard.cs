using System.Collections.Generic;

internal class ShuffleCard : AnokCard
{
    private IDollPlacementController _placementController;

    public override int Number => 5;
    public override string StringKey => "cards_anok_{0}_" + Number;

    public ShuffleCard() : base()
    {
        _placementController = ServiceLocator.Resolve<IDollPlacementController>();
    }

    public override void PlayEffect()
    {
        _placementController.GeneratePlaces();
    }
}