using System;
using System.Collections;
using UnityEngine;

internal class TableCameraRotation : MonoBehaviour, IInitializable
{

    //очень плохое взаимодействие с BaseRotatableAxis. Вычисление позиции должно производится тут.

    [SerializeField] private BaseRotatableAxis[] _rotatableObjects;
    [SerializeField] private float _duration;

    private Palette _palette;
    private IInputHandler _inputHandler;
    private IDollPlacementController _placementController;

    public void Initialize()
    {
        _palette = ServiceLocator.Resolve<Palette>();
        _placementController = ServiceLocator.Resolve<DollPlacementController>();
        _inputHandler = ServiceLocator.Resolve<InputHandler>();

        _inputHandler.ButtonPressed += OnButtonPressed;

        _placementController.PlacementChanged += InitiateAxes;

        InitiateAxes();
    }

    private void InitiateAxes()
    {
        foreach (BaseRotatableAxis axis in _rotatableObjects)
        {
            axis.Initiate(_palette, _placementController, _duration);
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
        foreach (var  axis in _rotatableObjects)
        {
            axis.QueueRotation(direction);
        }
    }

    private void OnDestroy()
    {
        _inputHandler.ButtonPressed -= OnButtonPressed;
        _placementController.PlacementChanged += Initialize;
    }
}
