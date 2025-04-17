using Assets.Code.Logic;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class FortuneManager : IService
{

    private Reward[] _currentList;
    public Reward[] CurrentList => _currentList;

    public System.Action ListGenerated;

    private FortunePool _fortunePool;
    private CardSystem _cardSystem;
    private GameProcess _gameProcess;
    private AnokCashData _cashData;
    private DiscardManager _discardManager;


    public FortuneManager(FortunePool fortunePool, CardSystem cardSystem, GameProcess gameProcess, AnokCashData anokCashData, DiscardManager discardManager)
    {
        _fortunePool = fortunePool;
        _cardSystem = cardSystem;
        _gameProcess = gameProcess;
        _cashData = anokCashData;
        _discardManager = discardManager;
    }

    public void GenerateNewList()
    {
        List<Reward> rewards = _fortunePool.RewardList.Select(x => x.Clone()).ToList(); ;
        List<int> indexes = new List<int>();
        for (int i = 0; i < rewards.Count; i++)
        {
            indexes.AddRange(Enumerable.Repeat(i, rewards[i].Count));
        }

        _currentList = new Reward[12];
        for (int i = 0; i < _currentList.Length; i++)
        {
            int index = indexes[Random.Range(0, indexes.Count)];
            _currentList[i] = rewards[index];
            _ = _currentList[i].Value;
            indexes.Remove(index);
        }

        ListGenerated?.Invoke();
    }

    public void ApplyReward(int number)
    {
        Debug.Log($"Выпало {number}");

        Reward reward = _currentList[number - 1];

        switch (reward.Type)
        {
            case RewardType.SpadeCard:
                Debug.Log($"Педро получил {reward.Value} пиковых карт.");
                _cardSystem.TakeCard(true, reward.Value, true);
                break;
            case RewardType.Card:
                if (reward.Value > 0)
                {
                    Debug.Log($"{(_gameProcess.ItsPedroTurn ? "Педро" : "Анок")} получил {reward.Value} карт.");
                    _cardSystem.TakeCard(_gameProcess.ItsPedroTurn, reward.Value);
                }
                else
                {
                    Debug.Log($"{(_gameProcess.ItsPedroTurn ? "Педро" : "Анок")} должен сбросить {reward.Value} карт.");
                    _discardManager.AddDiscard(_gameProcess.ItsPedroTurn, reward.Value);
                }
               
                break;
            case RewardType.RegenerateAnok:
                Debug.Log($"Анок получил {reward.Value} денег.");
                _cashData.ChangeCash(reward.Value);
                break;
            case RewardType.OrderTasties:
                Debug.Log($"Мы заказали вкусностей. Поставим рядом с куклой на {_gameProcess.CurrentPlaceNumber} часах");
                break;
        }
    }

    public enum RewardType
    {
        SpadeCard,
        Card,
        RegenerateAnok,
        OrderTasties
    }
}
