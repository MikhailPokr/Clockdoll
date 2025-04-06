using System.Collections;
using UnityEngine;

internal class TableCameraRotation : MonoBehaviour
{
    
    [SerializeField] private RotatableAxis[] _rotatableObjects;

    public void Initiate(PrefabPalette palette, SeccionData seccion)
    {
        foreach (var obj in _rotatableObjects)
        {
            obj.Initiate(palette, seccion, 1);
        }
    }

    public void StartRotate(bool clockwise)
    {
        foreach (var obj in _rotatableObjects)
        {
            obj.StartRotation(clockwise);
        }
    }

    public void EndRotate()
    {
        foreach (var obj in _rotatableObjects)
        {
            obj.StopRotation();
        }
    }
}
