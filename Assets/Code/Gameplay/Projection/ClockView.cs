using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class ClockView : MonoBehaviour, IInitializable
{
    private const float degrees = 360 / -12;
    private const float sub_degrees = degrees / 12;


    [SerializeField] private GameObject _mArrow;
    [SerializeField] private GameObject _hArrow;
    [Space]
    [SerializeField] private bool _isTableClock;
 
    private GameSubStateMachine _gameSubStateMachine;
    private ProjectionController _projectionController;

    public event Action<bool> ButtomPressed;


    public void Initialize()
    {
        _gameSubStateMachine = ServiceLocator.Resolve<GameSubStateMachine>();
        _gameSubStateMachine.SubStateChanged += OnTurnChanged;

        _projectionController = ServiceLocator.Resolve<ProjectionController>();
        ButtomPressed += _projectionController.OnClockClick;

        OnTurnChanged(_gameSubStateMachine.CurrentState, _gameSubStateMachine.CurrentPlaceNumber);
    }

    private void OnTurnChanged(GameSubState state, ClockNum num)
    {
        MoveArrow(num, (int)state);
    }

    private void MoveArrow(float hour, float minutes)
    {
        _mArrow.transform.eulerAngles = new Vector3(0, 0, minutes * degrees);
        _hArrow.transform.eulerAngles = new Vector3(0, 0, hour * degrees + minutes * sub_degrees);
    }

    public void Click()
    {
        ButtomPressed?.Invoke(_isTableClock);
    }

    private void OnDestroy()
    {
        _gameSubStateMachine.SubStateChanged -= OnTurnChanged;
    }
}
