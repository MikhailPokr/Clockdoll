using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

internal class CoreTicker : MonoBehaviour, IService
{
    StateMachine _stateMachine;
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        _stateMachine.Update();
    }
}