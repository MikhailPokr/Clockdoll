using System;
using TMPro;
using UnityEngine;

public class CashIndicator : MonoBehaviour, IInitializable
{
    [SerializeField] private TextMeshProUGUI _text;

    private IAnokCashData _cashData;
    public void Initialize()
    {
        _cashData = ServiceLocator.Resolve<IAnokCashData>();
        SignalBus.Subscribe<CashChangedSignal>(this, OnCashChanged);
        OnCashChanged();
    }

    private void OnCashChanged() => _text.text = $"{_cashData.CashAmount}$";
}
