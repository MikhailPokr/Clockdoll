using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    public Action HandUpdated;

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
        if (pedroCard)
            return new PedroCard(_palette, spades);
        else
            return new AnokCard(_palette);
    }
}