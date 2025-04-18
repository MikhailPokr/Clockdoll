using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class DollPlacementController : IDollPlacementController
{
   
    private Dictionary<ClockNum, ClockNum> _dollsTruePlace;
    
    private Dictionary<ClockNum, ClockNum> _dollsCurrentPlace;

    public Dictionary<ClockNum, ClockNum> DollsTruePlace => _dollsTruePlace;
    public Dictionary<ClockNum, ClockNum> DollsCurrentPlace => _dollsCurrentPlace;
    public ClockNum GetDollIndex(ClockNum place) => _dollsCurrentPlace[place];
    public ClockNum GetCurrentDollIndex() => _dollsCurrentPlace[_currentPlace];

    public ClockNum GetTrueDollPlace(ClockNum index) => _dollsTruePlace[index];

    private ClockNum _currentPlace;
    public ClockNum CurrentPlace => _currentPlace;

    public event System.Action<ClockNum> CurrentPlaceChanged;
    public event System.Action PlacementChanged;

    public DollPlacementController()
    {
        _currentPlace = ClockNum.MinValue;
    }

    public void SetCurrentDoll(ClockNum index)
    {
        _currentPlace = index;
        CurrentPlaceChanged?.Invoke(index);
    }

    public void Generate()
    {
        GenarateTruePositions();
        GeneratePlaces();
    }
    public void GenarateTruePositions()
    {
        _dollsTruePlace = new Dictionary<ClockNum, ClockNum>();
        List<int> places = Enumerable.Range(ClockNum.MinValue, ClockNum.MaxValue).ToList();
        for (int i = ClockNum.MinValue; i <= ClockNum.MaxValue; i++)
        {
            int place = Random.Range(0, places.Count); 
            _dollsTruePlace.Add(i, places[place]);
            places.Remove(places[place]);
        }
    }

    public void GeneratePlaces()
    {
        _dollsCurrentPlace = new Dictionary<ClockNum, ClockNum>();
        List<int> places = Enumerable.Range(1, ClockNum.MaxValue).ToList();
        for (int i = ClockNum.MinValue; i <= ClockNum.MaxValue; i++)
        {
            int place = Random.Range(0, places.Count);
            _dollsCurrentPlace.Add(places[place], i);
            places.Remove(places[place]);
        }
    }

    public void SetNewPlacement(Dictionary<ClockNum, ClockNum> newPlacement)
    {
        _dollsCurrentPlace = newPlacement;
        PlacementChanged?.Invoke();
    }
}
