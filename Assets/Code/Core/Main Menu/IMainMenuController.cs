using UnityEngine;
using System;

internal interface IMainMenuController : IService
{
    void OnPlayButtonClick();
    void OnLocalizationButtonClick(string localizationKey);
}