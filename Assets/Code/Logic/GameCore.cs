using System;
using UnityEngine;

internal class GameCore : MonoBehaviour
{
    [SerializeField] private PrefabPalette _prefabPalette;

    [SerializeField] private MonoBehaviour[] _initializable;

    private InputManager _inputManager;

    private void Awake()
    {
        ServiceLocator.Register(_prefabPalette);
        TableData tableData = new();
        tableData.GeneratePlacement();
        ServiceLocator.Register(tableData);
        ServiceLocator.Register<GameProcess>(new());
        _inputManager = ServiceLocator.Register<InputManager>(new());

        for (int i = 0; i < _initializable.Length; i++)
        {
            if (_initializable[i] is IInitializable)
                (_initializable[i] as IInitializable).Initialize();
            else
                throw new Exception("Массив _initializable нужно заполнять только классами, реализующие IInitializable");
        }
    }

    private void Update()
    {
        _inputManager.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ServiceLocator.Resolve<GameProcess>().OnTurnEnd();
        }
    }
}
