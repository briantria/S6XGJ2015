/*
 * developer     : brian g. tria
 * creation date : 2015.11.24
 * 
 */

using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour 
{
	public static readonly float ORTHO_SIZE = 7.68f;
	public static readonly float MAX_ORTHOSIZE = 23.24f;
	
	private Camera m_mainCamera;
	private float m_fZoomSpeed = 30.5f;
    
	protected void Awake ()
	{
		m_mainCamera = Camera.main;
	}
	
	protected void Update ()
	{
        if (AppFlowManager.Instance == null || AppFlowManager.Instance.CurrentAppState != AppState.OnGameScreen) { return; }
        if (GameManager.Instance == null || GameManager.Instance.CurrentGamePhase == GamePhase.Play) { return; }
	
		if (m_mainCamera.orthographicSize < MAX_ORTHOSIZE && Input.GetAxis ("Mouse ScrollWheel") < 0)
		{
			m_mainCamera.orthographicSize = Mathf.Min (m_mainCamera.orthographicSize + (m_fZoomSpeed * Time.deltaTime), MAX_ORTHOSIZE);
		}
		else if (m_mainCamera.orthographicSize > ORTHO_SIZE && Input.GetAxis ("Mouse ScrollWheel") > 0)
		{
			m_mainCamera.orthographicSize = Mathf.Max (m_mainCamera.orthographicSize - (m_fZoomSpeed * Time.deltaTime), ORTHO_SIZE);
		}
	}
}
