/*
 * developer     : brian g. tria
 * creation date : 2015.11.23
 * 
 */

using UnityEngine;
using System.Collections;

public class CameraDrag : MonoBehaviour 
{
	private const float m_fMinTimeSinceMouseDown = 0.1f;
	
	private Camera m_camera;
	private Transform m_cameraTransform;
	private Vector3 m_v3PrevMousePos;
	private Vector2 m_v2MousePosOffset;
	private float m_secondsSinceMouseDown;
	private float m_fScrollSpeed = 0.08f;
	private bool m_bDidMouseDown = false;
	
	protected void Awake ()
	{
		m_camera = Camera.main;
		m_cameraTransform = m_camera.transform;
		m_fScrollSpeed *= Screen.width;
	}
	
	protected void Update ()
	{
		if (GameManager.Instance.CurrentGamePhase == GamePhase.Play) { return; }
		
		if (Input.GetMouseButtonDown (0))
		{
			m_bDidMouseDown = true;
			m_secondsSinceMouseDown = Time.time;
		}
		
		if (Input.GetMouseButtonUp (0))
		{
			m_bDidMouseDown = false;
		}
		
		if (!m_bDidMouseDown)
		{
			return;
		}
		
		Vector3 v3MousePos = m_camera.ScreenToWorldPoint (Input.mousePosition) * Constants.PPU;
		Vector3 camPos = m_cameraTransform.position;
		
		if ((Time.time - m_secondsSinceMouseDown) < m_fMinTimeSinceMouseDown)
		{
			m_v3PrevMousePos = v3MousePos;
			return;
		}
		
		m_v2MousePosOffset = m_v3PrevMousePos - v3MousePos;
		camPos.x += (m_v2MousePosOffset.x * m_fScrollSpeed);
		camPos.y += (m_v2MousePosOffset.y * m_fScrollSpeed);
		m_cameraTransform.position = camPos;
		m_v3PrevMousePos = v3MousePos;
	}
}
