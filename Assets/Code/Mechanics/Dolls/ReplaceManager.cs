using System.Collections.Generic;
using System.Linq;

internal class ReplaceManager : IReplaceManager
{
    private bool _replaceActive;

    private IDollPlacementController _placementController;

    private List<ClockNum> _places;

    public ReplaceManager(IDollPlacementController placementController)
    {
        _placementController = placementController;
        _places = new List<ClockNum>();
    }

    public void AddPlace(int place)
    {
        _places.Add(place);
    }

    public void StartReplace()
    {
        Dictionary<ClockNum, ClockNum> result = new Dictionary<ClockNum, ClockNum>(_placementController.DollsCurrentPlace);

        (result[_places[0]], result[_places[1]]) = (result[_places[1]], result[_places[0]]);

        _placementController.SetNewPlacement(result);
    }

    public void RotateAll(bool clockwise)
    {
        var newPlacement = new Dictionary<ClockNum, ClockNum>();
        foreach (KeyValuePair<ClockNum, ClockNum> pair in _placementController.DollsCurrentPlace)
        {
            int newPos = pair.Value + (clockwise ? 1 : -1);
            newPlacement[pair.Key] = newPos;
        }
        _placementController.SetNewPlacement(newPlacement);
    }

    public void InsertDoll()
    {
        ClockNum currentDoll = _placementController.DollsCurrentPlace[_places[0]];
        if (_places[0] == _places[1])
        {
            _places.RemoveAt(1); //убираем, чтобы сразу можно было выбрать другой вариант
            return;
        }

        bool moveClockwise = (_places[1] - _places[0]) > 6;
        Dictionary<ClockNum, ClockNum> newPlacement = new Dictionary<ClockNum, ClockNum>(_placementController.DollsCurrentPlace);

        ClockNum place = _places[1];
        while (place != _places[0])
        {
            int nextPlace = place + (moveClockwise ? 1 : -1);
            ClockNum nextDoll = newPlacement.FirstOrDefault(x => x.Key == nextPlace).Value;
            newPlacement[place] = nextDoll;

            place = nextPlace;
        }

        newPlacement[_places[0]] = _places[1];
        _placementController.SetNewPlacement(newPlacement);
    }

    public void Shuffle()
    {
        _placementController.GeneratePlaces();
    }
}
