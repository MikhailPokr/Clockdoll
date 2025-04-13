using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TableData : IService
{
    public const int NumberOfPlayers = 12;

    private Dictionary<int, int> _dollsTruePlace;
    private Dictionary<int, int> _dollsCurrentPlace;

    public Dictionary<int, int> DollsCurrentPlace => _dollsCurrentPlace;
    public int GetDollIndex(int place) => _dollsCurrentPlace[place];
    public int GetCurrentDollIndex() => _dollsCurrentPlace[_currentPlace];

    public int GetTrueDollPlace(int index) => _dollsTruePlace[index];

    private int _currentPlace;
    public int CurrentPlace => _currentPlace;

    public System.Action<int> TableStartRotated;
    public System.Action<int> CurrentPlaceChanged;
    public System.Action PlacementChanged;

    public TableData()
    {
        _currentPlace = 1;
    }

    public void RotateTable(int direction) => TableStartRotated?.Invoke(direction);

    public void SetCurrentDoll(int index)
    {
        _currentPlace = index;
        CurrentPlaceChanged?.Invoke(index);
    }
    public void GeneratePlacement()
    {
        _dollsTruePlace = new Dictionary<int, int>();
        List<int> places = Enumerable.Range(1, NumberOfPlayers).ToList();
        for (int i = 1; i <= NumberOfPlayers; i++)
        {
            int place = Random.Range(0, places.Count);
            _dollsTruePlace.Add(i, places[place]);
            places.RemoveAt(place);
        }

        _dollsCurrentPlace = new Dictionary<int, int>();
        places = Enumerable.Range(1, NumberOfPlayers).ToList();
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
