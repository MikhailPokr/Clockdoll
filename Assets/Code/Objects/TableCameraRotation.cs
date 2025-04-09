using System.Collections;
using UnityEngine;

internal class TableCameraRotation : MonoBehaviour, IInitializable
{
    
    [SerializeField] private BaseRotatableAxis[] _rotatableObjects;
    [SerializeField] private float _duration;

    private InputManager _inputManager;

    public void Initialize()
    {
        PrefabPalette palette = ServiceLocator.Resolve<PrefabPalette>(); 
        TableData seccion = ServiceLocator.Resolve<TableData>();
        _inputManager = ServiceLocator.Resolve<InputManager>();
        _inputManager.ButtonPresed += OnButtonPressed;
        foreach (var obj in _rotatableObjects)
        {
            obj.Initiate(palette, seccion, _duration, 1);
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
        foreach (var obj in _rotatableObjects)
        {
            obj.QueueRotation(direction);
        }
    }

    private void OnDestroy()
    {
        _inputManager.ButtonPresed -= OnButtonPressed;
    }
}
