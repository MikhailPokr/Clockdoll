using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class JsonLoader : MonoBehaviour
{
    [System.Serializable]
    public class TextData
    {
        public string key;
        public string speaker;
        public string expression;
        public string content;

        public string[] variations;
    }

    [Serializable]
    public class TextDataDataList
    {
        public TextData[] data;
    }

    public static JsonLoader Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public List<TextData> fullData = new List<TextData>();
    public int GetDialogueLinesCount(string searchedKey) => fullData.Count(data => data.key.Contains(searchedKey));
    public void LoadJson() {
        string path = Path.Combine(Application.dataPath, "json");
        string[] files = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories);


        foreach (string filePath in files) {
            string json = File.ReadAllText(filePath);

            #if UNITY_EDITOR
            if (json == null) { 
                Debug.LogError($"An error occured while loading {filePath.Replace(Application.dataPath + "\\json", "")} json.");
                return; 
            } 
            Debug.Log($"{filePath.Replace(Application.dataPath, "")} loaded successfully!");
            #endif

            TextData[] dataList = JsonUtility.FromJson<TextData[]>(json);
            fullData.AddRange(dataList); 
        }
    }

    public void LoadJsonR()
    {
        TextAsset[] loadedAssets = Resources.LoadAll<TextAsset>("json");

        foreach (TextAsset file in loadedAssets)
        {
            TextDataDataList dataList = JsonUtility.FromJson<TextDataDataList>(file.text);
            fullData.AddRange(dataList.data);
        }
    }


    public TextData DataSearch(string searchedKey) {
        TextData searchedData;
        foreach (TextData data in fullData) {
            if (data.key == searchedKey) {
                searchedData = data;
                return searchedData;
            }
        }
        return null;
    }


}