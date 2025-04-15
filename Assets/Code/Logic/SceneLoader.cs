using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

internal class SceneLoader : IService
{
    private readonly CoreTicker _coreTicker;

    public SceneLoader(CoreTicker coreTicker)
    {
        _coreTicker = coreTicker;
    }

    public void Load(string sceneName, Action onLoad = null)
    {
        _coreTicker.StartCoroutine(LoadScene(sceneName, onLoad));
    }

    private IEnumerator LoadScene(string sceneName, Action onLoad)
    {
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(sceneName);

        while (!loadingScene.isDone)
            yield return null;

        onLoad?.Invoke();
    }

}