using System;
using UnityEngine;

internal class GameProcess : IService
{
    private bool _itsPedroTurn;
    public bool ItsPedroTurn => _itsPedroTurn;
    private int _currentPlaceNumber;
    public int CurrentPlaceNumber => _currentPlaceNumber;
    public Action CircleCompleted;

    public Action<bool, int> TurnChanged;

    public GameProcess()
    {
        _itsPedroTurn = true;
        _currentPlaceNumber = 1;
    }

    public void Start() => TurnChanged?.Invoke(_itsPedroTurn, _currentPlaceNumber);

    public void OnTurnEnd()
    {
        _itsPedroTurn = !_itsPedroTurn;
        if (_itsPedroTurn)
        {
            if (_currentPlaceNumber >= 12)
            {
                _currentPlaceNumber = 1;
                CircleCompleted?.Invoke();
            }
            else
            {
                _currentPlaceNumber++;
            }
        }
        TurnChanged?.Invoke(_itsPedroTurn, _currentPlaceNumber);
    }
}
