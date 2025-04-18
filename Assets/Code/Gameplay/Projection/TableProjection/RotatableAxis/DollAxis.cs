using System.Linq;
using UnityEngine;

internal class DollAxis : BaseRotatableAxis
{
    

    protected override GameObject Generate(ClockNum place)
    {
        int index = _placementController.GetDollIndex(place);
        DollView doll = Instantiate(_palette.DollsData.First(x => x.Index == index).Prefab, transform);
        int realPlace = _placementController.GetTrueDollPlace(index);
        doll.ChangeNumber(index);
        doll.name = $"Doll {doll.Index} in place: {place} (real place: {realPlace})";
        return doll.gameObject;
    }
}
