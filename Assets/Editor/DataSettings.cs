using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataSettings
{
    [MenuItem("Settings/Delete saves")]
    public static void DeleteSaves()
    {
        PlayerPrefs.DeleteKey(Constants.kPrefsFilename);
    }

}
