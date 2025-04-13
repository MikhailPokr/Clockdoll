
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

internal class FortuneList : MonoBehaviour, IInitializable
{
    [SerializeField] TextMeshProUGUI _text;
    private FortuneManager _manager;
    public void Initialize()
    {
        _manager = ServiceLocator.Resolve<FortuneManager>();

        _manager.ListGenerated += OnListGenerated;
    }

    private void OnListGenerated()
    {
        List<string> list = _manager.CurrentList.Select(x => string.Format(x.Description, x.Value)).ToList();

        string text = "";
        for (int i = 1; i < list.Count; i++)
        {
            text += $"{i+1}\n{list[i]}\n";
        }
        _text.text = text;
    }
}
