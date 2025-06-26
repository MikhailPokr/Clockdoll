internal class MainMenuController : IMainMenuController
{
    private LocalizationHandler _localizationHandler;

    public MainMenuController(LocalizationHandler localizationHandler)
    {
        _localizationHandler = localizationHandler;
    }

    public void OnPlayButtonClick()
    {
        SignalBus.Publish(new GameStartSignal());
    }
    public void OnLocalizationButtonClick(string localizationKey)
    {
        _localizationHandler.ChangeLocalizationKey(localizationKey);
    }
}