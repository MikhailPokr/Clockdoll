using System.Collections.Generic;

internal class ShuffleCard : AnokCard
{
    private IDollPlacementController _placementController;

    public ShuffleCard() : base()
    {
        _placementController = ServiceLocator.Resolve<IDollPlacementController>();

        _suit = _palette.Numbers[5];
    }

    public override void PlayEffect()
    {
        _placementController.GeneratePlaces();
    }
}