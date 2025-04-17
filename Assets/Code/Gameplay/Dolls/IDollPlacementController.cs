using System;
using System.Collections.Generic;

internal interface IDollPlacementController : IService
{
    int CurrentPlace { get; }
    Dictionary<int, int> DollsCurrentPlace { get; }

    void GenarateTruePositions();
    void Generate();
    void GeneratePlaces();
    int GetCurrentDollIndex();
    int GetDollIndex(int place);
    int GetTrueDollPlace(int index);
    void RotateTable(int direction);
    void SetCurrentDoll(int index);
    void SetNewPlacement(Dictionary<int, int> newPlacement);

    event Action<int> TableStartRotated;
    event Action<int> CurrentPlaceChanged;
    event Action PlacementChanged;
}