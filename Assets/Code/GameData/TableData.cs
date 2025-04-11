using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TableData : IService
{
    public const int NumberOfPlayers = 12;

    private Dictionary<int, int> _placesOfDolls;
    public int GetDollIndex(int place) => _placesOfDolls[place];

    private int _currentPlace;
    public int CurrentPlace => _currentPlace;

    public System.Action<int> TableStartRotated;
    public System.Action<int> TargetPlaceChanged;

    public TableData()
    {
        _currentPlace = 1;
    }

    public void RotateTable(int direction) => TableStartRotated?.Invoke(direction);

    public void SetCurrentDoll(int index)
    {
        _currentPlace = index;
        TargetPlaceChanged?.Invoke(index);
    }
    public void GeneratePlacement()
    {
        _placesOfDolls = new Dictionary<int, int>();
        List<int> places = Enumerable.Range(1, NumberOfPlayers).ToList();
        for (int i = 1; i <= NumberOfPlayers; i++)
        {
            int place = Random.Range(0, places.Count);
            _placesOfDolls.Add(places[place], i);
            places.RemoveAt(place);
        }
    }

    

}
