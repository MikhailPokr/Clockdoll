using System;

internal interface IGame : IService
{
    void Start();

    void CardClick(BaseCard card);
    void DiceTrayClick();
}