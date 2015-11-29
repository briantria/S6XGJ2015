/*
 * developer     : brian g. tria
 * creation date : 2015.10.28
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HomeHudManager : MonoBehaviour
{
    private Canvas m_canvas = null;
    
    protected void Awake ()
    {
        m_canvas = this.GetComponent<Canvas> ();
    }

	public void OnClickPlay ()
	{
		//UIFlowManager.Instance.UpdateUI (HudId.Game);
		AppFlowManager.Instance.AppStateUpdate (AppState.OnGameScreen);
	}
}