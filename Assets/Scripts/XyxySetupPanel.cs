/*
 * developer     : brian g. tria
 * creation date : 2015.11.11
 * 
 */

using UnityEngine;
using System.Collections;

public class XyxySetupPanel : MonoBehaviour 
{
	private static XyxySetupPanel m_instance = null;
	public  static XyxySetupPanel Instance {get {return m_instance;}}
	
	private RectTransform m_rectTransform;
	private GameObject m_gameObject;
	
	protected void Awake ()
	{
		m_instance = this;
		m_rectTransform = this.GetComponent<RectTransform> ();
		m_gameObject = this.gameObject;
		
		Close ();
	}
	
	public void Open ()
	{
		m_gameObject.SetActive (true);
	}
	
	public void Close ()
	{
		m_gameObject.SetActive (false);
	}
	
	public void SetPosition (Vector2 p_v2Position)
	{
		m_rectTransform.anchoredPosition = p_v2Position;
	}
}
