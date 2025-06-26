internal interface IMainMenuController : IService
{
    void OnPlayButtonClick();
    void OnLocalizationButtonClick(string localizationKey);
}