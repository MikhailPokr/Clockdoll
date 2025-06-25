using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cysharp.Threading.Tasks;

internal class SceneLoader : IService
{
    private readonly CoreTicker _coreTicker;

    public SceneLoader(CoreTicker coreTicker)
    {
        _coreTicker = coreTicker;
    }

    public void Load(string sceneName, Action onLoad = null)
    {
        LoadScene(sceneName, onLoad).Forget();
    }

    private async UniTask LoadScene(string sceneName, Action onLoad)
    {
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(sceneName);
        await loadingScene;
        onLoad?.Invoke();
    }
}