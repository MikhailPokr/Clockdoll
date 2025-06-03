using UnityEngine;

internal class MainMenu : MonoBehaviour, IInitializable
{
    private IMainMenuController _mainMenuController;
    private IDialogueBoxController _dialogueBoxController;

    public void Initialize()
    {
        _mainMenuController = ServiceLocator.Resolve<IMainMenuController>();
        _dialogueBoxController = ServiceLocator.Resolve<IDialogueBoxController>();
    }
    public void OnButtonClick()
    {
        _mainMenuController.OnPlayButtonClick();
    }
    public void OnLocalizationButtonClick(string localizationKey)
    {
        _mainMenuController.OnLocalizationButtonClick(localizationKey);
        _dialogueBoxController.CreateDialogueBoxByKey("cards_anok", 0);
    }
}