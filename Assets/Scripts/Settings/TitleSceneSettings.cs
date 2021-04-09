using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneSettings : SettingsManager
{
    private void Start()
    {
        filePath = Application.persistentDataPath + filePath;

        Load();
    }
}
