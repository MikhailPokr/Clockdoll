using UnityEngine;

internal class ClockView : MonoBehaviour, IInitializable
{
    private const float degrees = 360 / -12;
    private const float sub_degrees = degrees / 12;


    [SerializeField] private GameObject _mArrow;
    [SerializeField] private GameObject _hArrow;
    [Space]
    [SerializeField] private bool _isTableClock;


    public void Initialize()
    {
        SignalBus.Subscribe<SubStateChangedSignal>(this, OnTurnChanged);

        //OnTurnChanged(new(_gameSubStateMachine.CurrentState, _gameSubStateMachine.CurrentPlaceNumber));
    }

    private void OnTurnChanged(SubStateChangedSignal signal)
    {
        Debug.Log($"вертим стрелки на состояние {signal.GameSubState}");
        MoveArrow(signal.CurrentPlace, (int)signal.GameSubState);
    }

    private void MoveArrow(ClockNum hour, ClockNum minutes)
    {
        if (minutes == 12)
            hour--;
        //выглядит как костыль
        //но оно нужно, поскольку на момент начала хода педро кукла уже должна поменятся
        //но на часах это все все еще прошлый ход

        _mArrow.transform.eulerAngles = new Vector3(0, 0, minutes * degrees);

        _hArrow.transform.eulerAngles = new Vector3(0, 0, hour * degrees + minutes * sub_degrees);
    }

    public void Click()
    {
        SignalBus.Publish(new ClockPressedSignal(_isTableClock));
    }
}
