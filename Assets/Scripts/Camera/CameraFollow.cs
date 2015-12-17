/*
 * developer     : brian g. tria
 * creation date : 2015.11.23
 *
 */

using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
//	private Camera m_mainCamera;
	private Transform m_transform;
	private Transform m_playerTransform;
	
	protected void Awake ()
	{
//		m_mainCamera = Camera.main;
		m_transform = this.transform;
		m_playerTransform = PlayerController.Instance.transform;
	}

	protected void Update ()
	{
		if (GameManager.Instance != null
		&&  GameManager.Instance.CurrentGamePhase == GamePhase.Play)
		{
			Vector3 position = m_transform.position;
			position.x = m_playerTransform.position.x;
			position.y = m_playerTransform.position.y;
			m_transform.position = position;
		}
	}
}