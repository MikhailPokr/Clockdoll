using UnityEngine;

[RequireComponent(typeof(CoreTicker))]
internal class GameBootstrapper : MonoBehaviour
{
    [SerializeField] CoreTicker _coreTicker;
    private StateMachine _stateMachine;

    private void Start()
    {
        _stateMachine = ServiceLocator.Register(new StateMachine());

        ServiceLocator.Register(_coreTicker);
        _coreTicker.Initialize(_stateMachine);

        _stateMachine.ChangeState(new BootstrapState(_coreTicker, _stateMachine));
    }
}