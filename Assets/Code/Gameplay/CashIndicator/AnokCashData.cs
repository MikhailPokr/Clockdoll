using System;
using UnityEngine;

internal class AnokCashData : IService
{
    public const int MaxCash = 100;

    private int _cashAmount;
    public float CashAmount => _cashAmount;

    public Action CashChanged;
    public Action CashOver;

    public AnokCashData()
    {
        _cashAmount = MaxCash;
    }

    public void ChangeCash(int value)
    {
        _cashAmount += value;
        if (_cashAmount <= 0)
            CashOver?.Invoke();
        _cashAmount = Math.Clamp(_cashAmount, 0, MaxCash);
        CashChanged?.Invoke();
    }
}
