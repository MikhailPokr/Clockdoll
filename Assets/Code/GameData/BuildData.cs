using UnityEngine;
internal class BuildData : IService
{
    public string Version {  get; private set; }
    public RuntimePlatform Platform { get; private set; }

    public BuildData()
    {
        Version = Application.version;
        Platform = Application.platform;
    }
}
