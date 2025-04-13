using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

internal class HandData : IService
{
    private List<PedroCard>[] _pedroPlayed;
    private List<AnokCard> _anokPlayed;

    private List<PedroCard> _pedroHand;
    private List<AnokCard> _anokHand;

    private bool _showingPedroHand;
    private TableData _tableData;
    private GameProcess _gameProcess;
    private Palette _palette;

    public System.Action HandUpdated;

    public HandData(TableData tableData, GameProcess gameProcess, Palette palette)
    {
        _anokPlayed = new List<AnokCard>();
        _pedroPlayed = new List<PedroCard>[13];
        for (int i = 1; i <= 12; i++)
        {
            _pedroPlayed[i] = new();
        }
        _anokHand = new List<AnokCard>();
        _pedroHand = new List<PedroCard>();

        _tableData = tableData;
        _tableData.CurrentPlaceChanged += (_) => HandUpdated?.Invoke();
        _tableData.PlacementChanged += () => HandUpdated?.Invoke();

        _gameProcess = gameProcess;

        _palette = palette;

        _showingPedroHand = false;
    }

    public void SwitchHand()
    {
        _showingPedroHand = !_showingPedroHand;
        HandUpdated?.Invoke();
    }

    public List<BaseCard> GetHand(bool pedroHand)
    {
        if (pedroHand)
            return _pedroHand.Cast<BaseCard>().ToList();
        return _anokHand.Cast<BaseCard>().ToList();
    }
    public List<BaseCard> GetCurrentHand()
    {
        if ( _showingPedroHand )
            return _pedroPlayed[_tableData.CurrentPlace].Cast<BaseCard>().ToList();
        return _anokHand.Cast<BaseCard>().ToList();
    }

    public void TakeCard(bool toPedro, int count, bool spadesGuaranteed = false)
    {
        for (int i = 0; i < count; i++)
        {
            AddCard(GenerateCard(toPedro, spadesGuaranteed));
        }
    }

    public void AddCard(BaseCard card)
    {
        if (card is PedroCard)
            _pedroHand.Add((PedroCard)card);
        else
            _anokHand.Add((AnokCard)card);
        HandUpdated?.Invoke();
    }
    public void PlayCard(BaseCard card)
    {
        if (card is PedroCard)
        {
            _pedroHand.Remove((PedroCard)card);
            _pedroPlayed[_gameProcess.CurrentPlaceNumber].Add((PedroCard)card);
        }
        else
        {
            _anokHand.Remove((AnokCard)card);
            _anokPlayed.Add((AnokCard)card);
        }
        HandUpdated?.Invoke();
    }

    private BaseCard GenerateCard(bool pedroCard, bool spades)
    {
        int value;
        if (pedroCard)
        {
            value = Random.Range(spades ? 6 : 1, 9);
            BaseCondition condition = value switch
            {
                0 => new CloserCondition(),
                1 => new StepCondition(),
                2 => new PowerCondition(),
                3 => new PrimeCondition(),
                4 => new DigitCondition(),
                5 => new SideCondition(),
                //пики
                6 => new RouletteCondition(),
                7 => new NeighborCondition(),
                8 => new CloserCondition(),
                _ => null
            };

            Suit suit = condition.Suit;

            value = Random.Range(0, 4);
            BaseEffect effect = value switch
            {
                0 => new DamageEffect(),
                1 => new DiceAddEffect(),
                2 => new DrawCardEffect(),
                3 => new DiscardEffect(),
                _ => null
            };

            return new PedroCard(condition, effect);
        }   
        else
        {
            value = Random.Range(0, 10);
            AnokCard card = value switch
            {
                0 => new RotateCard(),
                1 => new InsertDollCard(),
                2 => new CountCard(),
                3 => new ShuffleCard(),
                4 => new BlockCard(),
                5 => new DefendCard(),
                6 => new RestoreCard(),
                7 => new DrawCardCard(),
                8 => new SkipCard(),
                9 => new EatCard(),
                _ => null,
            };

            return card;
        }
            
    }
}