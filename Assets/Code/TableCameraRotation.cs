using System.Collections;
using UnityEngine;

internal class TableCameraRotation : MonoBehaviour
{
    
    [SerializeField] private RotatableAxis[] _rotatableObjects;
    [SerializeField] private float _duration;

    public void Initiate(PrefabPalette palette, SeccionData seccion)
    {
        foreach (var obj in _rotatableObjects)
        {
            obj.Initiate(palette, seccion, _duration, 1);
        }
    }

    public void StartRotate(bool clockwise)
    {
        foreach (var obj in _rotatableObjects)
        {
            obj.QueueRotation(clockwise ? 1 : -1);
        }
    }

    public void EndRotate(bool clockwise)
    {
        foreach (var obj in _rotatableObjects)
        {
            obj.QueueRotation(0);
        }
    }
}
