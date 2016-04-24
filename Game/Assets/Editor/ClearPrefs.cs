using UnityEngine;
using System.Collections;
using UnityEditor;


public class ClearPrefs
{
    [MenuItem("Assets/Clear Player Prefs")]
    static void DeleteAllPlayerPrefs()
    {
        Caching.CleanCache();
        PlayerPrefs.DeleteAll();
    }
}