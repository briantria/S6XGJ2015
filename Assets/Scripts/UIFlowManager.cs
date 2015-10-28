/*
 * developer     : brian g. tria
 * creation date : 2015.10.27
 *
 */

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UIFlowManager : MonoBehaviour 
{
	private static UIFlowManager m_instance = null;
	public  static UIFlowManager Instance {get { return m_instance; }}
	
	public delegate void UIFlowUpdate (HudId p_hudID);
	public static event UIFlowUpdate onActivateHud;
	public static event UIFlowUpdate onDeactivateHud;
	
	#region PROPERTIES
	public HudId ActiveHUD { set; get; }
	#endregion
	
	#region CONSTANTS
	private const string UI_RESOURCE_PATH = "Prefabs/HUD";
	private readonly Dictionary <HudId, string> m_dictUIPrefabs = new Dictionary <HudId, string> ()
	{
		{HudId.Home,          "Home"},
		{HudId.Game,          "Game"},
		{HudId.Settings,      "Settings"},
		{HudId.LoadingScreen, "LoadingScreen"}
	};
	#endregion
	
	private Dictionary <HudId, GameObject> m_dictChildren = new Dictionary <HudId, GameObject> ();

	protected void Awake ()
	{
		if (m_instance == null)
		{
			m_instance = this;
		}
		
		UIPrefabInit ();
	}
	
	protected void Start ()
	{
		UpdateUI (HudId.Home);
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
				
				m_dictChildren.Add (go.GetComponent<HudManager>().ID, gObj);
			}
		}
	}
	
	public void UpdateUI (HudId p_newHudID)
	{
		if (onDeactivateHud != null)
		{
			onDeactivateHud (p_newHudID);
		}
		
		// refresh display
		foreach (KeyValuePair <HudId, GameObject> child in m_dictChildren)
		{
			GameObject gObj = child.Value;
			bool willActivate = (p_newHudID & gObj.GetComponent<HudManager>().ID) > 0;
			
			if (willActivate == false) { continue; }
			gObj.SetActive (willActivate);
		}
		
		if (onActivateHud != null)
		{
			onActivateHud (p_newHudID);
		}
		
		ActiveHUD = p_newHudID;
	}
	
	public string GetHudName (HudId p_hudID)
	{
		return m_dictUIPrefabs [p_hudID];
	}
}

[Flags]
public enum HudId 
{
	Home          = 1 << 0,
	Game          = 1 << 1,
	LoadingScreen = 1 << 2,
	Settings      = 1 << 3
}

