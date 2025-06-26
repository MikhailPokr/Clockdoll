using UnityEngine;

internal class LightAxis : BaseRotatableAxis
{
    protected override GameObject Generate(ClockNum place)
    {
        GameObject light = Instantiate(_palette.Light, transform).gameObject;
        return light;
    }
}
