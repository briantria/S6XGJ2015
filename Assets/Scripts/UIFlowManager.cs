/*
 * author : brian g. tria
 *   date : 2015.10.27
 *
 */


// https://msdn.microsoft.com/en-us/library/zk2z37d3.aspx

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIFlowManager : MonoBehaviour 
{
	private const string UI_RESOURCE_PATH = "Prefabs/UI";
	private readonly Dictionary <HudId, HudName> m_dictUIPrefabs = new Dictionary <HudId, HudName> ()
	{
		{HudId.Home,          new HudName (HudId.Home,          "Home")},
		{HudId.Game,          new HudName (HudId.Game,          "Game")},
		{HudId.Settings,      new HudName (HudId.Settings,      "Settings")},
		{HudId.LoadingScreen, new HudName (HudId.LoadingScreen, "LoadingScreen")}
	};

	private static UIFlowManager m_instance = null;
	public  static UIFlowManager Instance {get { return m_instance; }}
	
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
			if (m_dictUIPrefabs.ContainsKey (new HudName))
			{
				GameObject gObj = (GameObject) Instantiate (go);
				gObj.name = go.name;
				
				Transform gTrans = gObj.transform;
				gTrans.SetParent (this.transform);
			}
		}
	}
}

