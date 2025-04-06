using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeccionData
{
    public const int NumberOfPlayers = 12;

    private Dictionary<int, int> _placesOfDolls;
    public int GetDollIndex(int place) => _placesOfDolls[place];

    public void GeneratePlacement()
    {
        _placesOfDolls = new Dictionary<int, int>();
        List<int> places = Enumerable.Range(1, NumberOfPlayers).ToList();
        for (int i = 0; i < NumberOfPlayers; i++)
        {
            int place = Random.Range(0, places.Count);
            _placesOfDolls.Add(places[place], i);
            places.RemoveAt(place);
        }
    }

}
