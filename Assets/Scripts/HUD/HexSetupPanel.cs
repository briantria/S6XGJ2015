/*
 * developer     : brian g. tria
 * creation date : 2015.11.11
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HexSetupPanel : MonoBehaviour
{
	private static HexSetupPanel m_instance = null;
	public  static HexSetupPanel Instance {get {return m_instance;}}
    
    private List<GameObject> m_objChildren = new List<GameObject> ();
	private RectTransform m_rectTransform;
    private Canvas m_canvas;
	//private GameObject m_gameObject;
    private bool m_bAllowDisplay;
	
	protected void Awake ()
	{
		m_instance = this;
		m_rectTransform = this.GetComponent<RectTransform> ();
        m_canvas = this.GetComponent<Canvas> ();
		//m_gameObject = this.gameObject;
		
        foreach (Transform t in m_rectTransform)
        {
            m_objChildren.Add (t.gameObject);
        }
        
        m_bAllowDisplay = false;
		Close ();
	}
	
	public void Open ()
	{
		//m_gameObject.SetActive (true);
        m_canvas.enabled = m_bAllowDisplay;
        for (int idx = m_objChildren.Count-1; idx >= 0; --idx)
        {
            m_objChildren[idx].SetActive (m_bAllowDisplay);
        }
	}
	
	public void Close ()
	{
		//m_gameObject.SetActive (false);
        m_canvas.enabled = false;
        for (int idx = m_objChildren.Count-1; idx >= 0; --idx)
        {
            m_objChildren[idx].SetActive (false);
        }
	}
	
	public void SetPosition (Vector2 p_v2Position)
	{
		m_rectTransform.anchoredPosition = p_v2Position;
	}
}
