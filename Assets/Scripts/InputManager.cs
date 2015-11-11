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
}
