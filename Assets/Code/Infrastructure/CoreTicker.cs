using System;
using System.Collections;
using UnityEngine;

internal class CoreTicker : MonoBehaviour, IService
{
    StateMachine _stateMachine;
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;

        DontDestroyOnLoad(this);
    }

    public void Invoke(Action action, float delay)
    {
        if (action == null) return;
        StartCoroutine(ExecuteAfterDelay(action, delay));
    }

    private IEnumerator ExecuteAfterDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }

    private void Update()
    {
        _stateMachine.Update();
    }
}