using System.Collections.Generic;
using UnityEngine;

internal interface IDataLoader : IService
{
    Object LoadPrefab(string name);
    List<Object> LoadPrefabsFromGroup(string groupName);
    List<T> LoadJsonList<T>(string path);
}