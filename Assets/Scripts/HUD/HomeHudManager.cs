/*
 * developer     : brian g. tria
 * creation date : 2015.10.28
 *
 */

using UnityEngine;
using System.Collections;

public class HomeHudManager : MonoBehaviour// HudManager
{
	public void OnClickPlay ()
	{
		//UIFlowManager.Instance.UpdateUI (HudId.Game);
		AppFlowManager.Instance.AppStateUpdate (AppState.OnGameScreen);
	}
}