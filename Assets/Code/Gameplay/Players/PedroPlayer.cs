using UnityEngine;
internal  class PedroPlayer : IPedroPlayer
{
    private IGame _game;
    private IGameSubStateMachine _subStateMachine;
    public PedroPlayer(IGame game, IGameSubStateMachine subStateMachine)
    {
        _game = game;
        _subStateMachine = subStateMachine;

        _subStateMachine.SubStateChanged += OnStateChanged;
    }

    private void OnStateChanged(GameSubState state, ClockNum place)
    {
        if (state >= GameSubState.AnokReaction && state < GameSubState.PedroReaction)
            return;

        CoreTicker coreTicker = ServiceLocator.Resolve<CoreTicker>();

        switch (state)
        {
            case GameSubState.PedroReaction:
                {
                    coreTicker.Invoke(() => TestTurn("Педро сбрасывает карты"), 1);
                    break;
                }
            case GameSubState.PedroStartTurn:
                {
                    coreTicker.Invoke(() => TestTurn("Педро начинает ход"), 1);
                    break;
                }
            case GameSubState.PedroRollDice:
                {
                    coreTicker.Invoke(() => TestTurn("Педро кидает кубы"), 1);
                    break;
                }
            case GameSubState.PedroFortune:
                {
                    coreTicker.Invoke(() => TestTurn("Педро реагирует на награду"), 1);
                    break;
                }
            case GameSubState.PedroCardChoice:
                {
                    coreTicker.Invoke(() => TestTurn("Педро выбирает карту"), 1);
                    break;
                }
            case GameSubState.PedroCardPlay:
                {
                    coreTicker.Invoke(() => TestTurn("Педро играет карту"), 1);
                    break;
                }
        }
    }

    public void TestTurn(string text)
    {
        Debug.Log(text);
        _subStateMachine.GoToNextState();
        
    }


}
