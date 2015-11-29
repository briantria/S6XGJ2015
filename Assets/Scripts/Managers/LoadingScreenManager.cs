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
    private CanvasRenderer m_canvasRenderer = null;
    
    protected void Awake ()
    {
        m_instance = this;
        m_canvas = this.GetComponent<Canvas> ();
        m_canvasRenderer = this.GetComponent<CanvasRenderer> ();
    }
    
    public void Open ()
    {
        m_canvas.enabled = true;
        //StartCoroutine (FadeIn ());
    }
    
    public void Close ()
    {
        m_canvas.enabled = false;
        //StartCoroutine (FadeOut ());
    }
    
    private IEnumerator FadeIn ()
    {
        float alpha = 0.0f;
        float speed = 20.0f;
        m_canvasRenderer.SetAlpha (alpha);
        
        while (alpha < 1.0f)
        {
            alpha += (Time.deltaTime * speed);
            m_canvasRenderer.SetAlpha (alpha);
            alpha = m_canvasRenderer.GetAlpha ();
            yield return new WaitForEndOfFrame ();
        }
        
        m_canvasRenderer.SetAlpha (1.0f);
    }
    
    private IEnumerator FadeOut ()
    {
        float alpha = 1.0f;
        float speed = 10.0f;
        m_canvasRenderer.SetAlpha (alpha);
        
        while (alpha > 0.0f)
        {
            alpha -= (Time.deltaTime * speed);
            m_canvasRenderer.SetAlpha (alpha);
            alpha = m_canvasRenderer.GetAlpha ();
            yield return new WaitForEndOfFrame ();
        }
        
        m_canvasRenderer.SetAlpha (0.0f);
    }
}