/*
 * developer     : brian g. tria
 * creation date : 2015.11.26
 *
 */

using UnityEngine;
using System.Collections;

public class AppFlowManager : MonoBehaviour
{
	private static AppFlowManager m_instance = null;
    public  static AppFlowManager Instance {get {return m_instance;}}
    
    public delegate void  AppFlowAction (AppState p_appState);
    public static   event AppFlowAction appStateUpdate;
    
    private AppState m_currentAppState = AppState.OnHomeScreen;
    public AppState CurrentAppState {get {return m_currentAppState;}}
    
    protected void Awake ()
    {
    	m_instance = this;
    }
    
    public void AppStateUpdate (AppState p_appState)
    {
        if (appStateUpdate != null)
        {
            appStateUpdate (p_appState);
        }
        
        m_currentAppState = p_appState;
    }
}

[System.Flags]
public enum AppState
{
    OnLoadingScreen     = 1 << 0,
    OnHomeScreen        = 1 << 1,
    OnLevelSelectScreen = 1 << 2,
    OnGameScreen        = 1 << 3
}