using System.Collections;
using UnityEngine;

internal class TableCameraRotation : MonoBehaviour, IInitializable
{

    //очень плохое взаимодействие с BaseRotatableAxis. Вычисление позиции должно производится тут.

    [SerializeField] private BaseRotatableAxis[] _rotatableObjects;
    [SerializeField] private float _duration;

    private InputHandler _inputManager;
    private DollPlacementController _placementController;

    public void Initialize()
    {
        Palette palette = ServiceLocator.Resolve<Palette>();
        _placementController = ServiceLocator.Resolve<DollPlacementController>();
        _inputManager = ServiceLocator.Resolve<InputHandler>();
        _inputManager.ButtonPressed += OnButtonPressed;
        _placementController.PlacementChanged += Initialize;
        foreach (var obj in _rotatableObjects)
        {
            obj.Initiate(palette, _placementController, _duration);
        }
    }

    public void OnButtonPressed(KeyCode keyCode, int state)
    {
        if (keyCode == KeyCode.LeftArrow && state == 1)
            Rotate(-1);
        if (keyCode == KeyCode.RightArrow && state == 1)
            Rotate(1);
        if ((keyCode == KeyCode.LeftArrow || keyCode == KeyCode.RightArrow) && state == 1)
            Rotate(0);
    }

    public void Rotate(int direction)
    {
        _placementController.RotateTable(direction);
    }

    private void OnDestroy()
    {
        _inputManager.ButtonPressed -= OnButtonPressed;
    }
}
