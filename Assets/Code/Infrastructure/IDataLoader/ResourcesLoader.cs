using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static JsonLoader;
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


    public class JsonArrayWrapper<T>
    {
        public T[] data;
    }

    public List<T> LoadJsonList<T>(string path)
    {
        TextAsset[] loadedAssets = Resources.LoadAll<TextAsset>(directory + path);

        List<T> fullData = new List<T>();

        foreach (TextAsset file in loadedAssets)
        {
            JsonArrayWrapper<T> dataList = JsonUtility.FromJson<JsonArrayWrapper<T>>(file.text);
            fullData.AddRange(dataList.data);
        }

        return fullData;
    }
}