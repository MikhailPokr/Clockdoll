using UnityEngine;

internal class NumbersAxis : RotatableAxis
{
    protected override GameObject Generate(int place)
    {
        Number number = Instantiate(_palette.NumberPrefab, transform);
        number.ChangeNumber(place);
        return number.gameObject;
    }    
}
