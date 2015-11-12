/*
 * developer     : brian g. tria
 * creation date : 2015.11.12
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bolt : MonoBehaviour 
{
	[SerializeField] private LineRenderer m_lineRenderer;
	private List<Vector3> m_listLineVerteces = new List<Vector3> ();
	
	private const int m_iVertexCount = 4;
	private const float m_fMaxLengthX = 10.0f;
	
	private Transform m_transform;
	private Vector3 m_v3BgCamOrigin;
	
	private Vector3 m_v3InitPos;
	private Vector3 m_v3FinalPos;
	
	private Vector3 m_v3StartJoint;
	private Vector3 m_v3EndJoint;
	
	private float m_speed;
	private float m_fCurrLengthX;
	
	protected void Awake ()
	{
		m_transform = this.transform;
		m_v3BgCamOrigin = GameObject.FindGameObjectWithTag(Constants.BGCamTag).transform.position;
		m_v3BgCamOrigin.x -= Screen.width * Constants.PPU;
		m_v3BgCamOrigin.z = 0;
		
		m_lineRenderer.SetVertexCount (m_iVertexCount);
		ResetLineRenderer (m_v3BgCamOrigin);
		
		InitFromOrigin ();
	}
	
	private void ResetLineRenderer (Vector3 p_v3Pos)
	{
		m_listLineVerteces.Clear ();
		for (int idx = 0; idx < m_iVertexCount; ++idx)
		{
			m_lineRenderer.SetPosition (idx, p_v3Pos);
			m_listLineVerteces.Add (p_v3Pos);
		}
	}
	
	private void ExtendPulseLine (float p_fCurrLengthX, int p_iJointCount)
	{
		Vector3 v3InitPos = m_listLineVerteces[0];
		v3InitPos.x += p_fCurrLengthX;
		
		if (v3InitPos.x >= m_v3FinalPos.x)
		{
			m_lineRenderer.SetPosition (m_iVertexCount-1, m_v3FinalPos);
			m_listLineVerteces[m_iVertexCount-1] = m_v3FinalPos;
			
			return;
		}

		if (p_iJointCount == 0)
		{
			m_lineRenderer.SetPosition (m_iVertexCount-1, v3InitPos);
			m_listLineVerteces[m_iVertexCount-1] = v3InitPos;
		}
	}
	
	private void ContractPulseLine (float p_fCurrLengthX, int p_iJointCount)
	{
		Vector3 v3Head = m_listLineVerteces[m_iVertexCount-1];
		Vector3 v3Tail = m_listLineVerteces[0];
	
		Debug.Log ("p_fCurrLengthX < m_fMaxLengthX: " + (p_fCurrLengthX < m_fMaxLengthX));
		Debug.Log ("v3Head.x < m_v3FinalPos.x: " + (v3Head.x < m_v3FinalPos.x));
	
		if (p_fCurrLengthX < m_fMaxLengthX && v3Head.x < m_v3FinalPos.x)
		{
			return;
		}
		
		v3Tail.x += m_speed;
		
		if (v3Tail.x >= v3Head.x)
		{
			v3Tail.x = v3Head.x;
			m_fCurrLengthX = 0;
		}
		
		m_listLineVerteces[0] = v3Tail;
		m_lineRenderer.SetPosition (0, v3Tail);
		
		for (int idx = 1; idx < m_iVertexCount-1; ++idx)
		{
			if (v3Tail.x > m_listLineVerteces[idx].x)
			{
				m_listLineVerteces[idx] = v3Tail;
				m_lineRenderer.SetPosition (idx, v3Tail);
			}
		}
	}
	
	public void InitFromOrigin ()
	{
		Vector3 v3Origin = m_v3BgCamOrigin;
		v3Origin.x += Random.Range (-2.0f, 2.0f);
		v3Origin.y += Random.Range (-Screen.height * Constants.PPU * 0.5f, Screen.height * Constants.PPU * 0.5f);
		
		InitPulse (v3Origin);
	}
	
	public void InitPulse (Vector3 p_v3StartPoint)
	{
		Debug.Log ("RESET!");
		ResetLineRenderer (p_v3StartPoint);
		
		m_transform.position = p_v3StartPoint;
		m_v3InitPos = p_v3StartPoint;
		m_v3FinalPos = p_v3StartPoint;
		m_v3FinalPos.x += Random.Range (m_fMaxLengthX * 0.8f, m_fMaxLengthX * 1.5f);
		
		m_speed = Random.Range (5.0f, 8.0f) * Time.deltaTime;
		StartCoroutine ("Pulse");
	}
	
	#region Coroutines
	private IEnumerator Pulse ()
	{
		do
		{
			if (m_transform.position.x < m_v3FinalPos.x)
			{
				Vector3 v3CurrPos = m_transform.position;
				v3CurrPos.x += m_speed;
				m_transform.position = v3CurrPos;
				m_fCurrLengthX = v3CurrPos.x - m_listLineVerteces[0].x;
			}
			
			ExtendPulseLine (m_fCurrLengthX, 0);
			ContractPulseLine (m_fCurrLengthX, 0);
			
			yield return null;
		}
		while (m_fCurrLengthX > 0);
		
		Debug.Log ("FUCKING RESET!");
		InitFromOrigin ();
	}
	#endregion
}
