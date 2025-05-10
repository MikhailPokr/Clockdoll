using UnityEngine;

internal class AnokPlayer : IAnokPlayer
{
    private IGame _game;
    private IGameSubStateMachine _subStateMachine;
    public AnokPlayer(IGame game, IGameSubStateMachine subStateMachine)
    {
        _game = game;
        _subStateMachine = subStateMachine;

        _subStateMachine.SubStateChanged += OnStateChanged;
    }

    private void OnStateChanged(GameSubState state, ClockNum place)
    {
        if (state < GameSubState.AnokReaction || state >= GameSubState.PedroReaction)
            return;

        CoreTicker coreTicker = ServiceLocator.Resolve<CoreTicker>();

        switch (state)
        {
            case GameSubState.AnokReaction:
                {
                    coreTicker.Invoke(() => TestTurn("���� ���������� �����"), 1);
                    break;
                }
            case GameSubState.AnokStartTurn:
                {
                    coreTicker.Invoke(() => TestTurn("���� �������� ���"), 1);
                    break;
                }
            case GameSubState.AnokRollDice:
                {
                    coreTicker.Invoke(() => TestTurn("���� ������ ����"), 1);
                    break;
                }
            case GameSubState.AnokFortune:
                {
                    coreTicker.Invoke(() => TestTurn("���� ��������� �� �������"), 1);
                    break;
                }
            case GameSubState.AnokCardChoice:
                {
                    coreTicker.Invoke(() => TestTurn("���� �������� �����"), 1);
                    break;
                }
            case GameSubState.AnokCardPlay:
                {
                    coreTicker.Invoke(() => TestTurn("���� ������ �����"), 1);
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
