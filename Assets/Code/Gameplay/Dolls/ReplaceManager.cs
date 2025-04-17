using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class ReplaceManager : IService
{
    private bool _replaceActive;

    private DollPlacementController _placementController;

    private List<int> _places;

    public ReplaceManager(DollPlacementController placementController)
    {
        _placementController = placementController;
        _places = new List<int>();
    }

    public void AddPlace(int place)
    {
        _places.Add(place);
    }

    public void Replace()
    {
        Dictionary<int, int> result = new Dictionary<int, int>(_placementController.DollsCurrentPlace);

        (result[_places[0]], result[_places[1]]) = (result[_places[1]], result[_places[1]]);

        _placementController.SetNewPlacement(result);
    }

    public void RotateAll(bool clockwise)
    {
        var newPlacement = new Dictionary<int, int>();
        foreach (var pair in _placementController.DollsCurrentPlace)
        {
            int newPos = clockwise
                ? (pair.Value % 12) + 1
                : (pair.Value + 10) % 12 + 1;
            newPlacement[pair.Key] = newPos;
        }
        _placementController.SetNewPlacement(newPlacement);
    }

    public void InsertDoll()
    {
        var currentPlace = _placementController.DollsCurrentPlace[_places[0]];
        if (currentPlace == _places[1]) return;

        bool moveClockwise = (_places[1] - currentPlace + 12) % 12 > 6;
        var newPlacement = new Dictionary<int, int>(_placementController.DollsCurrentPlace);

        int place = _places[1];
        while (place != currentPlace)
        {
            int nextPlace = moveClockwise
                ? (place - 2 + 12) % 12 + 1
                : place % 12 + 1;

            var nextDoll = newPlacement.FirstOrDefault(x => x.Value == nextPlace).Key;
            if (nextDoll != 0) newPlacement[nextDoll] = place;

            place = nextPlace;
        }

        newPlacement[_places[0]] = _places[1];
        _placementController.SetNewPlacement(newPlacement);
    }
}
