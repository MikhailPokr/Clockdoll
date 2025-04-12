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
        TableData seccion = ServiceLocator.Resolve<TableData>();
        _inputManager = ServiceLocator.Resolve<InputManager>();
        _inputManager.ButtonPresed += OnButtonPressed;
        _tableData= ServiceLocator.Resolve<TableData>();
        foreach (var obj in _rotatableObjects)
        {
            obj.Initiate(palette, seccion, _duration);
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
        _inputManager.ButtonPresed -= OnButtonPressed;
    }
}
