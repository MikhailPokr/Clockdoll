using UnityEngine;

internal class GameCore : MonoBehaviour
{
    [SerializeField] private PrefabPalette _prefabPalette;
    [SerializeField] private TableCameraRotation _tableRotation;

    private void Start()
    { 
        SeccionData data = new SeccionData();
        data.GeneratePlacement();

        _tableRotation.Initiate(_prefabPalette, data);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            _tableRotation.StartRotate(false);
        if (Input.GetKey(KeyCode.RightArrow))
            _tableRotation.StartRotate(true);
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            _tableRotation.EndRotate(false);
        if (Input.GetKeyUp(KeyCode.RightArrow))
            _tableRotation.EndRotate(true);
    }
}
