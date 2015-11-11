/*
 * developer     : brian g. tria
 * creation date : 2015.11.11
 * 
 */

using UnityEngine;
using System.Collections;

public static class InputManager 
{
	private static Camera m_mainCamera = Camera.main;
	
	public static float ClickToTargetDistance (Vector2 p_v2TargetPosition)
	{
		Vector2 v2ClickPosition = m_mainCamera.ScreenToWorldPoint (Input.mousePosition);
		return (p_v2TargetPosition - v2ClickPosition).sqrMagnitude;
	}
	
	public static GameObject RaycastClick ()
	{
		RaycastHit2D hit = Physics2D.Raycast (m_mainCamera.ScreenToWorldPoint (Input.mousePosition), Vector2.zero, 0.0f);
		
		if (hit.collider != null)
		{
			return hit.collider.gameObject;
		}
		
		return null;
	}
	
	public static int OverlapClick ()
	{
		Collider2D [] results = new Collider2D [2];
		return Physics2D.OverlapPointNonAlloc (m_mainCamera.ScreenToWorldPoint (Input.mousePosition), results);
	}
}
