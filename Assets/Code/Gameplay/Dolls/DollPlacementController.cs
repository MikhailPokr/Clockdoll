using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class DollPlacementController : IDollPlacementController
{
    public const int NumberOfPlayers = 12;

    private Dictionary<int, int> _dollsTruePlace;
    private Dictionary<int, int> _dollsCurrentPlace;

    public Dictionary<int, int> DollsCurrentPlace => _dollsCurrentPlace;
    public int GetDollIndex(int place) => _dollsCurrentPlace[place];
    public int GetCurrentDollIndex() => _dollsCurrentPlace[_currentPlace];

    public int GetTrueDollPlace(int index) => _dollsTruePlace[index];

    private int _currentPlace;

    public event System.Action<int> TableStartRotated;
    public event System.Action<int> CurrentPlaceChanged;
    public event System.Action PlacementChanged;

    public int CurrentPlace => _currentPlace;

    public DollPlacementController()
    {
        _currentPlace = 1;
    }

    public void RotateTable(int direction) => TableStartRotated?.Invoke(direction);

    public void SetCurrentDoll(int index)
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
        _dollsTruePlace = new Dictionary<int, int>();
        List<int> places = Enumerable.Range(1, NumberOfPlayers).ToList();
        for (int i = 1; i <= NumberOfPlayers; i++)
        {
            int place = Random.Range(0, places.Count);
            _dollsTruePlace.Add(i, places[place]);
            places.RemoveAt(place);
        }
    }

    public void GeneratePlaces()
    {
        _dollsCurrentPlace = new Dictionary<int, int>();
        List<int> places = Enumerable.Range(1, NumberOfPlayers).ToList();
        for (int i = 1; i <= NumberOfPlayers; i++)
        {
            int place = Random.Range(0, places.Count);
            _dollsCurrentPlace.Add(places[place], i);
            places.RemoveAt(place);
        }
    }

    public void SetNewPlacement(Dictionary<int, int> newPlacement)
    {
        _dollsCurrentPlace = newPlacement;
        PlacementChanged?.Invoke();
    }

}
