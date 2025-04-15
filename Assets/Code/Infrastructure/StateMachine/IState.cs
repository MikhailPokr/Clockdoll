using UnityEngine;

internal interface IState
{
    void Enter();
    void Update();
    void Exit();
}