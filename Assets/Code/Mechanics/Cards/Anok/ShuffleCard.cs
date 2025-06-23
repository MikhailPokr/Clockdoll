using System.Collections.Generic;

internal class ShuffleCard : AnokCard
{
    private IDollPlacementController _placementController;

    public ShuffleCard() : base()
    {
        _placementController = ServiceLocator.Resolve<IDollPlacementController>();

        ApplyEffectText(5);
    }

    public override void PlayEffect()
    {
        _placementController.GeneratePlaces();
    }
}