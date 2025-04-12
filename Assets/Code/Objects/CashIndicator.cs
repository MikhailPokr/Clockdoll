using System;
using TMPro;
using UnityEngine;

public class CashIndicator : MonoBehaviour, IInitializable
{
    [SerializeField] private TextMeshProUGUI _text;

    private AnokCashData _cashData;
    public void Initialize()
    {
        _cashData = ServiceLocator.Resolve<AnokCashData>();
        _cashData.CashChanged += OnCashChanged;
        OnCashChanged();
    }

    private void OnCashChanged() => _text.text = $"{_cashData.CashAmount}$";
}
