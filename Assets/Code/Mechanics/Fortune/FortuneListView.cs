
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

internal class FortuneListView : MonoBehaviour, IInitializable
{
    [SerializeField] TextMeshProUGUI _text;
    private IFortuneSystem _manager;
    public void Initialize()
    {
        _manager = ServiceLocator.Resolve<IFortuneSystem>();

        _manager.ListGenerated += OnListGenerated;
    }

    private void OnListGenerated()
    {
        List<string> list = _manager.CurrentList.Select(x => string.Format(x.Value.Description, x.Value.Value)).ToList();

        string text = "";
        for (int i = 1; i < list.Count; i++)
        {
            text += $"{i+1}\n{list[i]}\n";
        }
        _text.text = text;
    }
}
