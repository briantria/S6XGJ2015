/*
 * developer     : brian g. tria
 * creation date : 2015.12.18
 * 
 */

using UnityEngine;
using System.Collections;

public class StartEndAnimation : MonoBehaviour 
{
	[SerializeField] private SpriteRenderer m_spriteRenderer;
	
	protected void Update ()
	{
		Color m_color = m_spriteRenderer.color;
		m_color.a = Mathf.PingPong (Time.time, 0.7f) + 0.3f;
		m_spriteRenderer.color = m_color;
	}
}
