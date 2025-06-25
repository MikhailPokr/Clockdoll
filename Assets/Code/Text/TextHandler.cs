using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

internal class TextHandler : ITextHandler
{
    private IDataLoader _dataLoader;
    private LocalizationHandler _localizationHandler;
    public List<TextData> _jsonData;

    public TextHandler(IDataLoader dataLoader, LocalizationHandler localizationController)
    {
        _dataLoader = dataLoader;
        _localizationHandler = localizationController;

        _jsonData = _dataLoader.LoadJsonList<TextData>("json");
    }

    public TextData ReturnJsonData(string jsonData, int page)
    {
        return DataSearch($"{jsonData}_{_localizationHandler.localizationKey}_{page}");
    }

    public TextData ReturnJsonData(string jsonData)
    {
        bool param = jsonData.Contains("]");
        string value = "";
        if (param)
        {
            int endBracketIndex = jsonData.IndexOf(']');
            string key = jsonData.Substring(endBracketIndex + 1);
            string inner = jsonData.Substring(1, endBracketIndex - 1);
            value = inner.Split(':').Last();
            jsonData = key;
        }
        TextData data = DataSearch(string.Format(jsonData, _localizationHandler.localizationKey));
        
        if (data == null)
        {
            data = new TextData() { content = string.Format(jsonData, _localizationHandler.localizationKey)};
            data.content = $"[rawKey{(param? "({0})" : "")}]" + data.content;
        }
        if (param)
            data.content = string.Format(data.content, value);
        return data;
    }

    public TextData DataSearch(string searchedKey)
    {
        TextData searchedData;
        foreach (TextData data in _jsonData)
        {
            if (data.key == searchedKey)
            {
                searchedData = data;
                return searchedData;
            }
        }
        return null;
    }
}