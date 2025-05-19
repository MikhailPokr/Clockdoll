using Assets.Code.Logic;
using System;
using System.Collections.Generic;
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

        if (_pedroPlayer is IBotPlayer)
        {
            var bot = (IBotPlayer)_pedroPlayer;
            bot.OnDiceTrayClickRequested += DiceTrayClick;
            bot.OnCardClickRequested += CardClick;
        }
        if (_anokPlayer is IBotPlayer)
        {
            var bot = (IBotPlayer)_anokPlayer;
            bot.OnDiceTrayClickRequested += DiceTrayClick;
            bot.OnCardClickRequested += CardClick;
        }
    }

    public void Start()
    {
        _gameSubStateMachine.Start();
        _placementController.Start();

        _anokPlayer.OnGameBegin();
        _pedroPlayer.OnGameBegin();
    }

    public void DiceTrayClick(bool isPedro)
    {
        if (isPedro != _gameSubStateMachine.IsPedroTurn) 
            return;

        IPlayer player = _gameSubStateMachine.IsPedroTurn ? _pedroPlayer : _anokPlayer;

        if  (player.OnTrayClick())
        {
            _gameSubStateMachine.GoToNextState();
        }
    }

    public void CardClick(BaseCard card)
    {
        if (!_cardSystem.IsCardInHand(card) && card != null)
            return;

        IPlayer player = _gameSubStateMachine.IsPedroTurn ? _pedroPlayer : _anokPlayer;

        if (player.OnCardClick(card))
        {
            _gameSubStateMachine.GoToNextState();
        }
    }

    public bool AlertClick()
    {
        _gameSubStateMachine.GoToNextState();
        return true;
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
                    player.EnterReactionState();
                    break;
                }
            case GameSubState.PedroStartTurn:
            case GameSubState.AnokStartTurn:
                {
                    _fortuneSystem.GenerateNewList();
                    player.EnterStartTurnState();
                    break;
                }
            case GameSubState.PedroRollDice:
            case GameSubState.AnokRollDice:
                {
                    player.EnterRollDiceState();
                    break;
                }
            case GameSubState.PedroFortune:
            case GameSubState.AnokFortune:
                {
                    player.EnterFortuneState();
                    break;
                }
            case GameSubState.PedroCardChoice:
            case GameSubState.AnokCardChoice:
                {
                    player.EnterCardChoiceState();
                    break;
                }
            case GameSubState.PedroCardPlay:
            case GameSubState.AnokCardPlay:
                {
                    player.EnterCardPlayState();
                    break;
                }
        }
    }
}