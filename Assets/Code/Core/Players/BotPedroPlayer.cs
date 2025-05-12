using UnityEngine;
internal  class BotPedroPlayer : IPedroPlayer
{
    public BotPedroPlayer()
    {
    }

    public void ReactionState()
    {
        //в основном сброс карт. Возможно что-то говорит.
    }

    public void StartState()
    {
        //может что-то сказать
    }

    public void RollDiceState()
    {
        //вызов ролла дайса
    }

    public void FortuneState()
    {
        //можно запоминать значение или просто использовать последнее. Отправлять его в фортуну. Этол стоит делать отдельно
    }

    public void CardChoiceState()
    {
        //выбор карты из списка. Занести это куда-нибудь 
    }

    public void CardPlayState()
    {
        //разыгровка карты
    }
}
