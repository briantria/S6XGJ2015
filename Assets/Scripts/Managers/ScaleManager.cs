/*
 * developer     : brian g. tria
 * creation date : 2015.11.30
 *
 * description   : make sprite seem to go far away as the camera zooms out
 *
 */

using UnityEngine;
using System.Collections;

public class ScaleManager : MonoBehaviour
{
    private RectTransform m_rectTransform = null;
	private Camera m_cameraZoom = null;
    private float m_prevOrthoSize;
    private float m_currOrthoSize;
    private Vector3 m_originalScale;
    
    protected void Awake ()
    {
        m_rectTransform = this.GetComponent<RectTransform> ();
        m_originalScale = m_rectTransform.localScale;
        
        m_cameraZoom = Camera.main;
        m_prevOrthoSize = m_cameraZoom.orthographicSize;
        m_currOrthoSize = m_cameraZoom.orthographicSize;
    }
    
    protected void Update ()
    {
        m_currOrthoSize = m_cameraZoom.orthographicSize;
        float deltaOrthoSize = Mathf.Abs (m_currOrthoSize - m_prevOrthoSize);
        if (deltaOrthoSize < 0.1f) { return; }
        
        m_rectTransform.localScale = m_originalScale * (CameraZoom.ORTHO_SIZE / m_currOrthoSize);
        m_prevOrthoSize = m_currOrthoSize;
        
        m_rectTransform.anchorMax = Vector2.one * ( m_currOrthoSize / CameraZoom.ORTHO_SIZE);
        m_rectTransform.anchorMin = Vector2.one * (1 - (m_currOrthoSize / CameraZoom.ORTHO_SIZE));
    }
}