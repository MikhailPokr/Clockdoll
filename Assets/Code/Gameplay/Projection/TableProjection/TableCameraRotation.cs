// TableCameraRotation.cs
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

internal class TableCameraRotation : MonoBehaviour, IInitializable
{
    [SerializeField] private BaseRotatableObject[] _rotatableObjects;
    [SerializeField] private float _duration;

    private Palette _palette;
    private IInputHandler _inputHandler;
    private IDollPlacementController _placementController;
    private bool _isRotating;

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
        if (_isRotating) return;

        if (keyCode == KeyCode.LeftArrow && state == 1)
            StartCoroutine(RotateTable(-1));
        else if (keyCode == KeyCode.RightArrow && state == 1)
            StartCoroutine(RotateTable(1));


    }

    private IEnumerator RotateTable(int direction)
    {
        _isRotating = true;
        bool clockwise = direction > 0;

        foreach (BaseRotatableAxis axis in _rotatableObjects)
        {
            axis.StartRotation(clockwise);
        }

        yield return new WaitWhile(() => _rotatableObjects.All(x => x.IsRotating));
        ClockNum newPlace = _placementController.CurrentPlace + direction;
        _placementController.SetCurrentDoll(newPlace);

        _isRotating = false;
    }

    private void OnDestroy()
    {
        _inputHandler.ButtonPressed -= OnButtonPressed;
        _placementController.PlacementChanged -= InitiateAxes;
    }
}