using UnityEngine;

internal class MainMenu : MonoBehaviour, IInitializable
{
    private IMainMenuController _mainMenuController;

    public void Initialize()
    {
        _mainMenuController = ServiceLocator.Resolve<IMainMenuController>();
    }
    public void OnButtonClick()
    {
        _mainMenuController.OnPlayButtonClick();
    }
    public void OnLocalizationButtonClick(string localizationKey)
    {
        _mainMenuController.OnLocalizationButtonClick(localizationKey);
    }
}