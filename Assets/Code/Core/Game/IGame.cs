internal interface IGame : IService
{
    void Start();

    void CardClick(bool isPedroTurn, BaseCard card);
    void DiceTrayClick(bool isPedroTurn);
    bool AlertClick();
}