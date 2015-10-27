/*
 * developer     : brian g. tria
 * creation date : 2015.10.28
 *
 */

using UnityEngine;
using System.Collections;

public class HomeHudManager : HudManager
{
	public void OnClickPlay ()
	{
		Debug.Log ("CLICK PLAY!");
		UIFlowManager.Instance.UpdateUI (HudId.Game);
	}
}