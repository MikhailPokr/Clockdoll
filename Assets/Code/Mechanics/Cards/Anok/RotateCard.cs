using UnityEngine;

internal class RotateCard : AnokCard
{
    private bool _isClockwise;

    public override int Number => 1;
    public override string StringKey => "cards_anok_{0}_" + Number + $"_{(_isClockwise ? 0 : 1)}";

    public RotateCard() : base()
    {
        _isClockwise = Random.value > 0.5f;
    }

    public override void PlayEffect(out IRequireLock requireLock)
    {
        requireLock = new DollRequireLock(false, 0, 
            _isClockwise ? DollRequireLockType.RotateClockwise : DollRequireLockType.RotateCounterclockwise);
    }
}