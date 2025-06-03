using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocalizationHandler : IService
{
    
    public string localizationKey;
    public void ChangeLocalizationKey(string key)
    {
        localizationKey = key;
    }
}
