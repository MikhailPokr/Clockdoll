using Cysharp.Threading.Tasks;
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
        _placementController = ServiceLocator.Resolve<IDollPlacementController>();
        _inputHandler = ServiceLocator.Resolve<IInputHandler>();

        SignalBus.Subscribe<InputSignal>(this, OnButtonPressed);
        SignalBus.Subscribe<PlacementChangedSignal>(this, InitiateAxes);

        InitiateAxes();
    }

    private void InitiateAxes()
    {
        foreach (BaseRotatableObject axis in _rotatableObjects)
        {
            axis.Initiate(_palette, _placementController, _duration);
        }
    }

    public void OnButtonPressed(InputSignal signal)
    {
        if (_isRotating) return;

        if (signal.KeyCode == KeyCode.LeftArrow && signal.PressState == 1)
            RotateTable(-1).Forget();
        else if (signal.KeyCode == KeyCode.RightArrow && signal.PressState == 1)
            RotateTable(1).Forget();
    }

    private async UniTask RotateTable(int direction)
    {
        _isRotating = true;
        bool clockwise = direction > 0;

        foreach (BaseRotatableObject axis in _rotatableObjects)
        {
            axis.StartRotation(clockwise);
        }

        await UniTask.WaitUntil(() => _rotatableObjects.All(x => !x.IsRotating));

        ClockNum newPlace = _placementController.CurrentPlace + direction;
        _placementController.SetCurrentDoll(newPlace);

        _isRotating = false;
    }
}