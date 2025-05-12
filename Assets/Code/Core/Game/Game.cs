using Assets.Code.Logic;
using System;
using UnityEngine;

internal class Game : IGame
{
    private IInputHandler _inputHandler;
    private IGameSubStateMachine _gameSubStateMachine;
    private IDollPlacementController _placementController;
    private IDiceController _diceManager;
    private IAnokCashData _cashData;
    private ICardSystem _cardSystem;
    private IDiscardManager _discardManager;
    private IFortuneSystem _fortuneSystem;
    private IDialogueSystem _dialogueSystem;
    private IAnokPlayer _anokPlayer;
    private IPedroPlayer _pedroPlayer;

    public Game(
        IInputHandler inputHandler,
        IGameSubStateMachine gameSubStateMachine,
        IDollPlacementController placementController,
        IDiceController diceManager,
        IAnokCashData cashData,
        ICardSystem cardSystem,
        IDiscardManager discardManager,
        IFortuneSystem fortuneSystem,
        IDialogueSystem dialogueSystem,
        IAnokPlayer anokPlayer,
        IPedroPlayer pedroPlayer)
    {
        _inputHandler = inputHandler;
        _gameSubStateMachine = gameSubStateMachine;
        _placementController = placementController;
        _diceManager = diceManager;
        _cashData = cashData;
        _cardSystem = cardSystem;
        _discardManager = discardManager;
        _fortuneSystem = fortuneSystem;
        _dialogueSystem = dialogueSystem;
        _anokPlayer = anokPlayer;
        _pedroPlayer = pedroPlayer;

        _gameSubStateMachine.SubStateChanged += OnStateChanged;
    }

    public void Start()
    {
        _gameSubStateMachine.Start();
        _placementController.Start();
    }

    public void DiceTrayClick()
    {

    }

    public void CardClick(BaseCard card)
    {
        
    }

    public void OnStateChanged(GameSubState state, ClockNum place)
    {
        CoreTicker coreTicker = ServiceLocator.Resolve<CoreTicker>();

        IPlayer player = _gameSubStateMachine.IsPedroTurn ? _pedroPlayer : _anokPlayer; 

        switch (state)
        {
            case GameSubState.PedroReaction:
            case GameSubState.AnokReaction:
                {
                    coreTicker.Invoke(() => TestTurn($"Реакция {(_gameSubStateMachine.IsPedroTurn ? "Педро" : "Aнока")}"), 1);
                    player.ReactionState();
                    break;
                }
            case GameSubState.PedroStartTurn:
            case GameSubState.AnokStartTurn:
                {
                    coreTicker.Invoke(() => TestTurn($"Начало хода {(_gameSubStateMachine.IsPedroTurn ? "Педро" : "Aнока")}"), 1);
                    player.StartState();
                    break;
                }
            case GameSubState.PedroRollDice:
            case GameSubState.AnokRollDice:
                {
                    coreTicker.Invoke(() => TestTurn($"{(_gameSubStateMachine.IsPedroTurn ? "Педро" : "Aнок")} роллит на фортуну"), 1);
                    player.RollDiceState();
                    break;
                }
            case GameSubState.PedroFortune:
            case GameSubState.AnokFortune:
                {
                    coreTicker.Invoke(() => TestTurn($"{(_gameSubStateMachine.IsPedroTurn ? "Педро" : "Aнок")} получает приз"), 1);
                    player.FortuneState();
                    break;
                }
            case GameSubState.PedroCardChoice:
            case GameSubState.AnokCardChoice:
                {
                    coreTicker.Invoke(() => TestTurn($"{(_gameSubStateMachine.IsPedroTurn ? "Педро" : "Aнок")} выбирает карту"), 1);
                    player.CardChoiceState();
                    break;
                }
            case GameSubState.PedroCardPlay:
            case GameSubState.AnokCardPlay:
                {
                    coreTicker.Invoke(() => TestTurn($"{(_gameSubStateMachine.IsPedroTurn ? "Педро" : "Aнок")} играет карту"), 1);
                    player.CardPlayState();
                    break;
                }
        }
    }

    private void TestTurn(string text)
    {
        //Debug.Log(text);
        _gameSubStateMachine.GoToNextState();
    }
}