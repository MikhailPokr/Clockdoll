using System.Linq;
using UnityEngine;

internal class DollAxis : BaseRotatableAxis
{
    

    protected override GameObject Generate(int place)
    {
        int index = _tableData.GetDollIndex(place);
        DollModel doll = Instantiate(_palette.DollsData.First(x => x.Index == index).Prefab, transform);
        int realPlace = _tableData.GetTrueDollPlace(index);
        doll.ChangeNumber(index);
        doll.name = $"Doll {doll.Index} in place: {place} (real place: {realPlace})";
        return doll.gameObject;
    }
}
