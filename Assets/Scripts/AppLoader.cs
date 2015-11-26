/*
 * developer     : brian g. tria
 * creation date : 2015.11.25
 * 
 */

using UnityEngine;
using System.Collections;

public class AppLoader : MonoBehaviour 
{
	protected void Start ()
	{
		AppFlowManager.Instance.AppStateUpdate (AppState.OnLoadingScreen);
		StartCoroutine ("LoadApp");
	}
	
	private IEnumerator LoadApp ()
	{
		#region LOAD ALL CANVAS
		GameObject[] canvasObjectArray = Resources.LoadAll<GameObject> ("Prefabs/Canvas");
		
		for (int idx = canvasObjectArray.Length-1; idx >= 0; --idx)
		{
			GameObject obj = Instantiate<GameObject> (canvasObjectArray[idx]);
            obj.name = canvasObjectArray[idx].name;
            yield return new WaitForSeconds (0.01f);
		}
		#endregion
		
		AppFlowManager.Instance.AppStateUpdate (AppState.OnHomeScreen);
	}
}
