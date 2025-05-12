using System;
using UnityEngine;

internal class AnokCashData : IAnokCashData
{
    private readonly int _maxCash;

    private int _cashAmount;
    public float CashAmount => _cashAmount;

    public event Action CashChanged;
    public event Action CashOver;

    public AnokCashData(int maxCash)
    {
        _maxCash = maxCash;
        _cashAmount = maxCash;
    }

    public void ChangeCash(int value)
    {
        _cashAmount += value;
        if (_cashAmount <= 0)
            CashOver?.Invoke();
        _cashAmount = Math.Clamp(_cashAmount, 0, _maxCash);
        CashChanged?.Invoke();
    }
}
