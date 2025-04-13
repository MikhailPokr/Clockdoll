using System.Collections;
using UnityEngine;

internal class TableCameraRotation : MonoBehaviour, IInitializable
{

    //очень плохое взаимодействие с BaseRotatableAxis. Вычисление позиции должно производится тут.

    [SerializeField] private BaseRotatableAxis[] _rotatableObjects;
    [SerializeField] private float _duration;

    private InputManager _inputManager;
    private TableData _tableData;

    public void Initialize()
    {
        Palette palette = ServiceLocator.Resolve<Palette>();
        _tableData = ServiceLocator.Resolve<TableData>();
        _inputManager = ServiceLocator.Resolve<InputManager>();
        _inputManager.ButtonPressed += OnButtonPressed;
        _tableData.PlacementChanged += Initialize;
        foreach (var obj in _rotatableObjects)
        {
            obj.Initiate(palette, _tableData, _duration);
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
        _tableData.RotateTable(direction);
    }

    private void OnDestroy()
    {
        _inputManager.ButtonPressed -= OnButtonPressed;
    }
}
