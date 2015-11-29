/*
 * developer     : brian g. tria
 * creation date : 2015.11.25
 * 
 */

using UnityEngine;
using System.Collections;

public class AppLoader : MonoBehaviour 
{
	protected void Awake ()
	{
		StartCoroutine ("LoadApp");
	}
	
	private IEnumerator LoadApp ()
	{
        // TODO: use AppState enum and loop
        //       dictionary vs Enum.Parse
    
		#region LOAD HOME SCREEN OBJECTS
		GameObject[] canvasObjectArray = Resources.LoadAll<GameObject> ("Prefabs/OnHomeScreen");
		for (int idx = canvasObjectArray.Length-1; idx >= 0; --idx)
		{
			GameObject obj = Instantiate<GameObject> (canvasObjectArray[idx]);
            obj.name = canvasObjectArray[idx].name;
            
            DisplayManager displayMngr = obj.AddComponent<DisplayManager> ();
            displayMngr.RequiredAppState = AppState.OnHomeScreen;
            
            yield return new WaitForEndOfFrame ();
		}
		#endregion
		
        #region LOAD ALL GAME SCREEN OBJECTS
        GameObject[] transformObjectArray = Resources.LoadAll<GameObject> ("Prefabs/OnGameScreen");
        for (int idx = transformObjectArray.Length-1; idx >= 0; --idx)
        {
            GameObject obj = Instantiate<GameObject> (transformObjectArray[idx]);
            obj.name = transformObjectArray[idx].name;
            
            DisplayManager displayMngr = obj.AddComponent<DisplayManager> ();
            displayMngr.RequiredAppState = AppState.OnGameScreen;
            
            yield return new WaitForEndOfFrame ();
        }
        #endregion
        
        #region LOAD MAZE POOL
        
        #endregion
        
		AppFlowManager.Instance.AppStateUpdate (AppState.OnHomeScreen);
        
        yield return new WaitForSeconds (0.2f);
        LoadingScreenManager.Instance.Close ();
	}
}
