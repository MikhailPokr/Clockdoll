using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class TextHandler : ITextHandler
{
    private IDataLoader _dataLoader;
    private LocalizationHandler _localizationHandler;
    private Palette _palette;
    private CoreTicker _coreticker;
    private int _index;
    private string _textUniqueKey;
    public List<TextData> _jsonData;

    public TextHandler(IDataLoader dataLoader, LocalizationHandler localizationController, CoreTicker coreTicker, Palette palette)
    {
        _dataLoader = dataLoader;
        _palette = palette;
        _coreticker = coreTicker;
        _localizationHandler = localizationController;

        _jsonData = _dataLoader.LoadJsonList<TextData>("json");
    }

    public TextData ReturnJsonData(string jsonData, int page)
    {
        _textUniqueKey = jsonData;
        _index = page;
        return DataSearch($"{_textUniqueKey}_{_localizationHandler.localizationKey}_{_index}");
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