/*
 * developer     : brian g. tria
 * creation date : 2015.11.30
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenManager : MonoBehaviour
{
	private static LoadingScreenManager m_instance = null;
    public  static LoadingScreenManager Instance {get {return m_instance;}}
    
    private Canvas m_canvas = null;
    
    protected void Awake ()
    {
        m_instance = this;
        m_canvas = this.GetComponent<Canvas> ();
    }
    
    public void Open ()
    {
        m_canvas.enabled = true;
    }
    
    public void Close ()
    {
        m_canvas.enabled = false;
    }
}