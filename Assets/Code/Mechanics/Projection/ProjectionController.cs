using UnityEngine;

internal class ProjectionController : IProjectionController
{
    private bool _isTopView;
    private Animator _animator;


    public ProjectionController(Animator animator)
    {
        _isTopView = false;
        _animator = animator;

        SignalBus.Subscribe<ClockPressedSignal>(this, signal => ChangeView(signal.ItsTableClock));
    }

    public void ChangeView(bool isTableClock)
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime < 1.0f)
            return;
        _isTopView = !isTableClock;
        _animator.SetTrigger("Click");
        _animator.SetBool("TableClick", isTableClock);
        SignalBus.Publish(new ViewModeChangedSignal(_isTopView));
    }

}
