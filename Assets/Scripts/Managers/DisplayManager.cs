/*
 * developer     : brian g. tria
 * creation date : 2015.11.30
 *
 */

using UnityEngine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisplayManager : MonoBehaviour
{
	private Canvas m_canvas;
    private Transform m_transform;
    private List<GameObject> m_children = new List<GameObject> ();
    
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
        m_transform = this.transform;
        
        foreach (Transform t in m_transform)
        {
            m_children.Add (t.gameObject);
        }
    }
    
    private void AppStateUpdate (AppState p_appState)
    {
        bool bEnable = (p_appState & RequiredAppState) > 0;
    
        if (m_canvas != null)
        {
            m_canvas.enabled = bEnable;
            Canvas.ForceUpdateCanvases ();
        }
        
        foreach (GameObject gObj in m_children)
        {
            gObj.SetActive (bEnable);
        }
    }
}