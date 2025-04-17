using System;

internal interface IAnokCashData : IService
{
    float CashAmount { get; }

    event Action CashChanged;
    event Action CashOver;

    void ChangeCash(int value);
}