using System;
using UnityEngine;

public class SpotLight : MonoBehaviour, IInitializable
{
    private TableData _tableData;
    public void Initialize()
    {
        _tableData = ServiceLocator.Resolve<TableData>();
        _tableData.TargetPlaceChanged += OnRotate;
        transform.localEulerAngles = new(0, 0, _tableData.CurrentPlace * -30);
    }

    private void OnRotate(int index)
    {
        transform.localEulerAngles = new(0, 0, index * -30);

    }
}
