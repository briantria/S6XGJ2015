/*
 * developer     : brian g. tria
 * creation date : 2015.11.11
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPanelSlide : MonoBehaviour 
{
	[SerializeField] private RectTransform m_rtPanel;
	
	private const float m_fInitPanelAnchorMinX = 0.0f;
	private const float m_fInitPanelAnchorMaxX = 0.3f;

	private Canvas m_canvas;
	private bool m_bIsVisible;
	private float m_fPanelAnchorMinX;
	private float m_fPanelAnchorMaxY;
	
	protected void Awake ()
	{
		m_bIsVisible = false;
		m_canvas = this.gameObject.GetComponent<Canvas> ();
	}

	public void ToggleDisplay ()
	{
		m_bIsVisible = !m_bIsVisible;
		Vector2 anchorMin = m_rtPanel.anchorMin;
		Vector2 anchorMax = m_rtPanel.anchorMax;
		
		if (m_bIsVisible)
		{
			anchorMin.x = m_fInitPanelAnchorMinX;
			anchorMax.x = m_fInitPanelAnchorMaxX;
			m_canvas.sortingOrder = 1;
		}
		else
		{
			anchorMin.x = -m_fInitPanelAnchorMaxX;
			anchorMax.x = m_fInitPanelAnchorMinX;
			m_canvas.sortingOrder = 0;
		}
		
		m_rtPanel.anchorMin = anchorMin;
		m_rtPanel.anchorMax = anchorMax;
	}
}
