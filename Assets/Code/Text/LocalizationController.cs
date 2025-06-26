public class LocalizationHandler : IService
{

    public string localizationKey;
    public void ChangeLocalizationKey(string key)
    {
        localizationKey = key;
    }
}
