using UnityEngine;

internal class LightAxis : BaseRotatableAxis
{
    protected override GameObject Generate(int place)
    {
        GameObject light = Instantiate(_palette.Light, transform).gameObject;
        return light;
    }    
}
