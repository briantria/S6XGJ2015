/*
 * developer     : brian g. tria
 * creation date : 2015.11.12
 * 
 */

using UnityEngine;
using System.Collections;

public class Bolt : MonoBehaviour 
{
	[SerializeField] private TrailRenderer m_trailRenderer;
	
	private Transform m_transform;
	private Vector3 m_v3InitPos;
	private Vector3 m_v3FinalPos;
	
	protected void Awake ()
	{
		m_transform = this.transform;
	}
	
	protected void Start ()
	{
		m_v3InitPos = m_v3FinalPos = m_transform.position = new Vector3 (3, 8, 0);
		m_v3FinalPos.x += Random.Range (10, 15);
		StartCoroutine ("Pulse");
	}
	
	private IEnumerator Pulse ()
	{
		float fSpeedX = Random.Range (2.0f, 3.0f);
		float fDistanceX = m_v3FinalPos.x - m_v3InitPos.x;
	
		while (m_transform.position.x < m_v3FinalPos.x)
		{
			Vector3 v3CurrPos = m_transform.position;
			v3CurrPos.x = Mathf.Lerp (m_v3InitPos.x, m_v3FinalPos.x, ((v3CurrPos.x - m_v3InitPos.x) / fDistanceX) + (fSpeedX * Time.deltaTime));
			m_transform.position = v3CurrPos;
			
			yield return null;
		}
		
		StartCoroutine ("Pause");
	}
	
	private IEnumerator Pause ()
	{
		yield return new WaitForSeconds (m_trailRenderer.time);
		
		m_trailRenderer.enabled = false;
		m_v3InitPos = m_v3FinalPos = m_transform.position = m_v3InitPos;
		m_v3FinalPos.x += Random.Range (10, 15);
	
		yield return new WaitForSeconds (0.1f);
		m_trailRenderer.enabled = true;
		StartCoroutine ("Pulse");
	}
}
