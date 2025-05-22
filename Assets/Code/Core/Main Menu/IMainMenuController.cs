using UnityEngine;
using System;

internal interface IMainMenuController : IService
{
    event Action OnGameStart;
    void OnPlayButtonClick();
    void OnLocalizationButtonClick(string localizationKey);
}
