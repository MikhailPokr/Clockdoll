using Assets.Code.Logic;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class FortuneSystem : IFortuneSystem
{
    private Dictionary<int, Reward> _currentList;
    public Dictionary<int, Reward> CurrentList => _currentList;

    public event System.Action ListGenerated;
    public event System.Action<Reward> RewardReceived;

    private FortunePool _fortunePool;
    private ICardSystem _cardSystem;
    private IGameSubStateMachine _gameSubStateMachine;
    private IAnokCashData _cashData;
    private IDiscardManager _discardManager;

    private int _diceEdges;
    public int DiceEdges => _diceEdges;


    public FortuneSystem(
        FortunePool fortunePool,
        ICardSystem cardSystem,
        IGameSubStateMachine gameSubStateMachine,
        IAnokCashData anokCashData,
        IDiscardManager discardManager,
        int diceSides
        )
    {
        _fortunePool = fortunePool;
        _cardSystem = cardSystem;
        _gameSubStateMachine = gameSubStateMachine; 
        _cashData = anokCashData;
        _discardManager = discardManager;
        _diceEdges = diceSides;
    }

    public void GenerateNewList()
    {
        List<Reward> rewards = _fortunePool.RewardList.Select(x => x.Clone()).ToList(); ;
        List<int> indexes = new List<int>();
        for (int i = 0; i < rewards.Count; i++)
        {
            indexes.AddRange(Enumerable.Repeat(i, rewards[i].Count));
        }

        _currentList = new();
        for (int i = 1; i <= _diceEdges; i++)
        {
            int index = indexes[Random.Range(0, indexes.Count)];
            _currentList[i] = rewards[index];
            _currentList[i].Lock();
            indexes.Remove(index);
        }

        ListGenerated?.Invoke();
    }

    public void ApplyReward(int number)
    {
        Reward reward = _currentList[number];

        switch (reward.Type)
        {
            case RewardType.SpadeCard:
                _cardSystem.TakeCard(true, reward.Value, true);
                break;
            case RewardType.Card:
                if (reward.Value > 0)
                {
                    _cardSystem.TakeCard(_gameSubStateMachine.IsPedroTurn, reward.Value);
                }
                else
                {
                    _discardManager.AddDiscard(_gameSubStateMachine.IsPedroTurn, reward.Value);
                }
                break;
            case RewardType.RegenerateAnok:
                _cashData.ChangeCash(reward.Value);
                break;
            case RewardType.OrderTasties:
                Debug.Log($"Мы заказали вкусностей. Поставим рядом с куклой на {_gameSubStateMachine.CurrentPlaceNumber} часах");
                break;
        }

        RewardReceived?.Invoke(reward);
    }
}
