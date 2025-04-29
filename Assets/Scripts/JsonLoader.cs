using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class JsonLoader : MonoBehaviour
{
    [System.Serializable]
    public class DialoguePageData {
        public string key;
        public string speaker;
        public string expression;
        public string content;
    }

    [Serializable]
    public class DialoguePageDataList
    {
        public DialoguePageData[] data;
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

    public List<DialoguePageData> fullData = new List<DialoguePageData>();
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

            DialoguePageData[] dataList = JsonUtility.FromJson<DialoguePageData[]>(json);
            fullData.AddRange(dataList); 
        }
    }

    public void LoadJsonR()
    {
        TextAsset[] loadedAssets = Resources.LoadAll<TextAsset>("json");

        foreach (TextAsset file in loadedAssets)
        {
            DialoguePageDataList dataList = JsonUtility.FromJson<DialoguePageDataList>(file.text);
            fullData.AddRange(dataList.data);
        }
    }


    public DialoguePageData DataSearch(string searchedKey) {
        DialoguePageData searchedData;
        foreach (DialoguePageData data in fullData) {
            if (data.key == searchedKey) {
                searchedData = data;
                return searchedData;
            }
        }
        return null;
    }


}
