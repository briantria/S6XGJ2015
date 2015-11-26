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
	private Canvas m_canvas = null;
    private IAppFlowListener m_interfaceAppFlowListener = null;
    
    public AppState RequiredAppState { get; set; }
	
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
        m_interfaceAppFlowListener = this.GetComponent<IAppFlowListener> ();
	}
	
	private void AppStateUpdate (AppState p_appState)
	{
        bool bAllowDisplay = (p_appState & RequiredAppState) > 0;
    
		if (m_canvas != null)
		{
            m_canvas.enabled = bAllowDisplay;
		}
        
        if (m_interfaceAppFlowListener != null)
        {
            m_interfaceAppFlowListener.AllowDisplay (bAllowDisplay);
        }
	}
}
