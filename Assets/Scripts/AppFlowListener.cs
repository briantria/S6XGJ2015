/*
 * developer     : brian g. tria
 * creation date : 2015.11.26
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class AppFlowListener : MonoBehaviour 
{
	[SerializeField] private AppState m_validAppState;
	private Canvas m_canvas = null;
	
	protected void OnEnable ()
	{
		AppFlowManager.appStateUpdate += AppStateUpdate;
	}
	
	protected void OnDisable ()
	{
		AppFlowManager.appStateUpdate -= AppStateUpdate;
	}
	
	protected void Awake ()
	{
		m_canvas = this.GetComponent<Canvas> ();
	}
	
	private void AppStateUpdate (AppState p_appState)
	{
		if (m_canvas != null)
		{
			m_canvas.enabled = (p_appState & m_validAppState) > 0;
		}
	}
}
