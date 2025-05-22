using UnityEngine;
using System;


internal class MainMenuController : IMainMenuController
{
    public event Action OnGameStart;
    private IDialogueSystem _dialogueSystem;

    public MainMenuController(IDialogueSystem dialogueSystem)
    {
        _dialogueSystem = dialogueSystem;
    }

    public void OnPlayButtonClick()
    {
        OnGameStart?.Invoke();
    }
    public void OnLocalizationButtonClick(string localizationKey)
    {
        _dialogueSystem.ChangeLocalizationKey(localizationKey);
    }
}
