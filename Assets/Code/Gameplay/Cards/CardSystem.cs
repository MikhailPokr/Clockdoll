using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class CardSystem : ICardSystem
{
    private List<PedroCard>[] _pedroPlayed;
    private List<AnokCard> _anokPlayed;

    private List<PedroCard> _pedroHand;
    private List<AnokCard> _anokHand;

    private bool _showingPedroHand;
    private IDollPlacementController _placementController;
    private Palette _palette;

    public System.Action HandUpdated;

    public CardSystem(IDollPlacementController placementController, Palette palette)
    {
        _anokPlayed = new List<AnokCard>();
        _pedroPlayed = new List<PedroCard>[12];
        for (int i = 1; i <= 12; i++)
        {
            _pedroPlayed[i] = new();
        }
        _anokHand = new List<AnokCard>();
        _pedroHand = new List<PedroCard>();

        _placementController = placementController;
        _placementController.CurrentPlaceChanged += (_) => HandUpdated?.Invoke();
        _placementController.PlacementChanged += () => HandUpdated?.Invoke();

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
        if (_showingPedroHand)
            return _pedroPlayed[_placementController.CurrentPlace].Cast<BaseCard>().ToList();
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
    public bool TryPlayCard(BaseCard card, ClockNum doll)
    {
        if (!card.CheckCondition())
            return false;
        if (card is PedroCard)
        {
            _pedroHand.Remove((PedroCard)card);
            _pedroPlayed[doll].Add((PedroCard)card);
        }
        else
        {
            _anokHand.Remove((AnokCard)card);
            _anokPlayed.Add((AnokCard)card);
        }
        card.PlayEffect();
        HandUpdated?.Invoke();
        return true;
    }

    public void DiscardCard(BaseCard card)
    {
        if (card is PedroCard)
        {
            _pedroHand.Remove((PedroCard)card);
        }
        else
        {
            _anokHand.Remove((AnokCard)card);
        }
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

            value = Random.Range(0, 3);
            BaseEffect effect = value switch
            {
                0 => new DamageEffect(suit),
                1 => new DrawCardEffect(suit),
                2 => new DiscardEffect(suit),
                //не реализованно
                3 => new DiceAddEffect(suit),
                _ => null
            };

            return new PedroCard(condition, effect);
        }
        else
        {
            value = Random.Range(0, 4);
            AnokCard card = value switch
            {
                0 => new RotateCard(),
                1 => new InsertDollCard(),
                2 => new ShuffleCard(),
                3 => new DrawCardCard(),
                //не реализованно
                4 => new EatCard(),
                5 => new DefendCard(),
                6 => new CountCard(),
                7 => new BlockCard(),
                8 => new RestoreCard(),
                9 => new SkipCard(),
                _ => null,
            };

            return card;
        }

    }
}