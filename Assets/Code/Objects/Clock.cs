using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class Clock : MonoBehaviour, IInitializable
{
    private const float degrees = 360 / -12;
    private const float sub_degrees = degrees / 12;


    [SerializeField] private GameObject _mArrow;
    [SerializeField] private GameObject _hArrow;
 
    private GameProcess _gameProcess;


    public void Initialize()
    {
        _gameProcess = ServiceLocator.Resolve<GameProcess>();
        _gameProcess.TurnChanged += OnTurnChanged;

        OnTurnChanged(_gameProcess.ItsPedroTurn, _gameProcess.CurrentPlaceNumber);
    }

    private void OnTurnChanged(bool is12, int hour)
    {
        MoveArrow(hour, is12 ? 0 : 6);
    }

    private void MoveArrow(float hour, float minutes)
    {
        _mArrow.transform.eulerAngles = new Vector3(0, 0, minutes * degrees);
        _hArrow.transform.eulerAngles = new Vector3(0, 0, hour * degrees + minutes * sub_degrees);
    }

    public void Click()
    {
        GetComponentInParent<Animator>().SetTrigger("Click");
    }

    private void OnDestroy()
    {
        _gameProcess.TurnChanged -= OnTurnChanged;
    }
}
