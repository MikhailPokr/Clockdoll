using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class FortuneManager : IService
{
    private FortunePool _fortunePool;

    private Reward[] _currentList;
    public Reward[] CurrentList => _currentList;

    public System.Action ListGenerated;

    public FortuneManager(FortunePool fortunePool)
    {
        _fortunePool = fortunePool;
    }

    public void GenerateNewList()
    {
        List<Reward> rewards = _fortunePool.RewardList;
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

    public enum RewardType
    {
        SpadeCard,
        Card,
        RegenerateAnok,
        OrderTasties
    }
}
