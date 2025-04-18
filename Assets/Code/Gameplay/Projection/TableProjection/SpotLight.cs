using System;
using UnityEngine;

public class SpotLight : MonoBehaviour, IInitializable
{
    private DollPlacementController _placementController;
    public void Initialize()
    {
        _placementController = ServiceLocator.Resolve<DollPlacementController>();
        _placementController.CurrentPlaceChanged += OnRotate;
        _placementController.PlacementChanged += () => OnRotate(_placementController.CurrentPlace);
        transform.localEulerAngles = new(0, 0, _placementController.CurrentPlace * -30);
    }

    private void OnRotate(ClockNum index)
    {
        transform.localEulerAngles = new(0, 0, index * -30);

    }
}
