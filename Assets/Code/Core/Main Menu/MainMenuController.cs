using UnityEngine;
using System;


internal class MainMenuController : IMainMenuController
{
    public event Action OnGameStart;
    private LocalizationHandler _localizationHandler;

    public MainMenuController(LocalizationHandler localizationHandler)
    {
        _localizationHandler = localizationHandler;
    }

    public void OnPlayButtonClick()
    {
        OnGameStart?.Invoke();
    }
    public void OnLocalizationButtonClick(string localizationKey)
    {
        _localizationHandler.ChangeLocalizationKey(localizationKey);
    }
}