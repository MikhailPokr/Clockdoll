using System.Collections.Generic;

internal class ShuffleCard : AnokCard
{
    private DollPlacementController _placementController;

    public ShuffleCard() : base()
    {
        _placementController = ServiceLocator.Resolve<DollPlacementController>();

        _suit = _palette.Numbers[5];
    }

    public override void PlayEffect()
    {
        _placementController.GeneratePlaces();
    }
}