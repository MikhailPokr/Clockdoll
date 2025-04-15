using System.Collections.Generic;
using UnityEngine;
using UnityObject = UnityEngine.Object;

internal class ResourcesLoader : IDataLoader
{
    private const string directory = "LoadableObjects/";
    public UnityObject LoadPrefab(string name)
    {
        return Resources.Load<UnityObject>(directory + name);
    }
    public List<UnityObject> LoadPrefabsFromGroup(string groupName)
    {
        UnityObject[] loadedAssets = Resources.LoadAll(directory + groupName);
        return new List<UnityObject>(loadedAssets);
    }
}