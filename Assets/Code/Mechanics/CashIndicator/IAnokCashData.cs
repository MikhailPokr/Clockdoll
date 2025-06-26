using System;

internal interface IAnokCashData : IService
{
    float CashAmount { get; }

    void ChangeCash(int value);
}