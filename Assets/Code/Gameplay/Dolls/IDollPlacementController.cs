using System;
using System.Collections.Generic;

internal interface IDollPlacementController : IService
{
    ClockNum CurrentPlace { get; }
    /// <summary>
    /// Место - Кукла
    /// </summary>
    Dictionary<ClockNum, ClockNum> DollsCurrentPlace { get; }
    /// <summary>
    /// Кукла - Реальное Место
    /// </summary>
    Dictionary<ClockNum, ClockNum> DollsTruePlace { get; }
    /// <summary>
    ///  Кукла по месту
    /// </summary>
    ClockNum GetDollIndex(ClockNum place);
    /// <summary>
    /// Индекс куклы на текущем месте 
    /// </summary>
    ClockNum GetCurrentDollIndex();
    /// <summary>
    /// Настоящее место куклы по кукле
    /// </summary>
    ClockNum GetTrueDollPlace(ClockNum index);

    void Generate();
    void GenarateTruePositions();
    void GeneratePlaces();

    void RotateTable(int direction);
    void SetCurrentDoll(ClockNum index);
    void SetNewPlacement(Dictionary<ClockNum, ClockNum> newPlacement);

    event Action<int> TableStartRotated;
    event Action<ClockNum> CurrentPlaceChanged;
    event Action PlacementChanged;
}