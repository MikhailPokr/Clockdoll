using System.Collections.Generic;
using UnityEngine;

internal class ReplaceManager : IService
{
    private bool _replaceActive;

    private TableData _tableData;

    public ReplaceManager(TableData tableData)
    {
        _tableData = tableData;
    }

    public void Replace(int firstPlace, int seconPlace)
    {
        Dictionary<int, int> result = new Dictionary<int, int>(_tableData.DollsCurrentPlace);

        (result[firstPlace], result[seconPlace]) = (result[seconPlace], result[firstPlace]);

        _tableData.SetNewPlacement(result);
    }
}
