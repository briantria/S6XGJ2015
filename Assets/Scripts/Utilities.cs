/*
 * developer     : brian g. tria
 * creation date : 2015.11.21
 *
 */

using UnityEngine;
using System.IO;
using System.Collections;

public static class Utilities
{
	public static string GetDataPath ()
    {
        #if    UNITY_EDITOR
        return Application.dataPath;
        #elif (UNITY_WEBGL || UNITY_WEBPLAYER)
        // to consider: Application.streamingAssetsPath
        return Application.absoluteURL;
        #elif (UNITY_IOS || UNITY_ANDROID)
        return Application.persistentDataPath;
        #endif
    }
    
    public static string GetGameDataDirectory ()
    {
        string strPath = GetDataPath () + "/GameData";
        if (Directory.Exists (strPath) == false)
        {
            Directory.CreateDirectory (strPath);
        }
        
        return strPath;
    }
}