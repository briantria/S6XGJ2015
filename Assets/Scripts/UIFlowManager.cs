/*
 * author : brian g. tria
 *   date : 2015.10.27
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIFlowManager : MonoBehaviour 
{
	private static UIFlowManager m_instance = null;
	public  static UIFlowManager Instance {get { return m_instance; }}
	
	private const string UI_RESOURCE_PATH = "Prefabs/HUD";
	private readonly Dictionary <HudId, string> m_dictUIPrefabs = new Dictionary <HudId, string> ()
	{
		{HudId.Home,          "Home"},
		{HudId.Game,          "Game"},
		{HudId.Settings,      "Settings"},
		{HudId.LoadingScreen, "LoadingScreen"}
	};

	protected void Awake ()
	{
		if (m_instance == null)
		{
			m_instance = this;
		}
		
		UIPrefabInit ();
	}
	
	private void UIPrefabInit ()
	{
		GameObject [] objArrUIPrefabs = Resources.LoadAll <GameObject> (UI_RESOURCE_PATH);
				
		foreach (GameObject go in objArrUIPrefabs)
		{
			if (m_dictUIPrefabs.ContainsValue (go.name))
			{
				GameObject gObj = (GameObject) Instantiate (go);
				gObj.name = go.name;
				
				Transform gTrans = gObj.transform;
				gTrans.SetParent (this.transform);
			}
		}
	}
	
	public string GetHudName (HudId p_hudID)
	{
		return m_dictUIPrefabs [p_hudID];
	}
}

public enum HudId 
{
	Home,
	Game,
	LoadingScreen,
	Settings
}

