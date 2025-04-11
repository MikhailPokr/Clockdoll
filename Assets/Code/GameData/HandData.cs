using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class HandData : IService
{
    private List<PedroCard>[] _pedroDiscard;
    private List<AnokCard> _anokDiscard;

    private List<PedroCard> _pedroHand;
    private List<AnokCard> _anokHand;

    private bool _showingPedroHand;
    private TableData _tableData;
    private GameProcess _gameProcess;

    public Action HandUpdated;

    public HandData(TableData tableData, GameProcess gameProcess)
    {
        _anokDiscard = new List<AnokCard>();
        _pedroDiscard = new List<PedroCard>[13];
        for (int i = 1; i <= 12; i++)
        {
            _pedroDiscard[i] = new();
        }
        _anokHand = new List<AnokCard>();
        _pedroHand = new List<PedroCard>();

        _tableData = tableData;
        _tableData.TargetPlaceChanged += (_) => HandUpdated?.Invoke();

        _gameProcess = gameProcess;

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
            return _pedroDiscard[_tableData.CurrentPlace].Cast<BaseCard>().ToList();
        return _anokHand.Cast<BaseCard>().ToList();
    }

    public void TakeCard(bool toPedro, int count)
    {
        for (int i = 0; i < count; i++)
        {
            AddCard(GenerateDollCard(toPedro));
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
            _pedroDiscard[_gameProcess.CurrentPlaceNumber].Add((PedroCard)card);
        }
        else
        {
            _anokHand.Remove((AnokCard)card);
            _anokDiscard.Add((AnokCard)card);
        }
        HandUpdated?.Invoke();
    }

    private BaseCard GenerateDollCard(bool pedroCard)
    {
        if (pedroCard)
            return new PedroCard();
        else
            return new AnokCard();
    }
}