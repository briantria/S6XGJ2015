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
    private HexButtonManager m_listener;
	
	protected void Awake ()
	{
		m_instance = this;
		m_rectTransform = this.GetComponent<RectTransform> ();
        m_canvas = this.GetComponent<Canvas> ();
		
        foreach (Transform t in m_rectTransform)
        {
            m_objChildren.Add (t.gameObject);
        }
        
		Close ();
	}
	
	public void Open ()
	{
        m_canvas.enabled = true;
        for (int idx = m_objChildren.Count-1; idx >= 0; --idx)
        {
            m_objChildren[idx].SetActive (true);
        }
	}
	
	public void Close ()
	{
        m_listener = null;
        m_canvas.enabled = false;
        for (int idx = m_objChildren.Count-1; idx >= 0; --idx)
        {
            m_objChildren[idx].SetActive (false);
        }
	}
	
    public void OnChoosePlayerType (string p_strPlayerType)
    {
        PlayerType playerType = (PlayerType) System.Enum.Parse (typeof (PlayerType), p_strPlayerType);
        m_listener.OnHexSetupPanelResult (playerType);
        Close ();
    }
    
	public void SetHexSetupListener (HexButtonManager p_hexBtn)
	{
        m_listener = p_hexBtn;
		m_rectTransform.anchoredPosition = p_hexBtn.transform.position;
	}
}
