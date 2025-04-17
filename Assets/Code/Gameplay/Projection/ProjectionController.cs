using System;
using UnityEngine;

internal class ProjectionController : IProjectionController
{
    private bool _isTopView;
    private Animator _animator;

    public event Action<bool> ViewModeChanged;


    public ProjectionController(Animator animator)
    {
        _isTopView = false;
        _animator = animator;
    }

    public void OnClockClick(bool isTableClock)
    {
        ChangeView(isTableClock);
    }

    public void ChangeView(bool isTableClock)
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime < 1.0f)
            return;
        _isTopView = !isTableClock;
        _animator.SetTrigger("Click");
        _animator.SetBool("TableClick", isTableClock);
        ViewModeChanged?.Invoke(_isTopView);
    }

}
