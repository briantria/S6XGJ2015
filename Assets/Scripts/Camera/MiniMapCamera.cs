/*
 * developer     : brian g. tria
 * creation date : 2015.12.17
 * 
 */

using UnityEngine;
using System.Collections;

public class MiniMapCamera : MonoBehaviour 
{
	[SerializeField] private Camera m_miniMapCamera;
	
	protected void OnEnable ()
	{
		PlayerController.OnPlayerComboUpdate += OnPlayerComboUpdate;
	}
	
	protected void OnDisable ()
	{
		PlayerController.OnPlayerComboUpdate -= OnPlayerComboUpdate;
	}
	
	private void OnPlayerComboUpdate (PlayerType p_playerType)
	{
		bool bEnableMiniMap = (GameManager.Instance != null && (GamePhase.Play == GameManager.Instance.CurrentGamePhase) && (p_playerType & PlayerType.Geexy) > 0);
		m_miniMapCamera.enabled = bEnableMiniMap;
	}
}
