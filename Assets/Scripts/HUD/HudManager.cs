/*
 * developer     : brian g. tria
 * creation date : 2015.10.28
 *
 */

using UnityEngine;
using System.Collections;

public class HudManager : MonoBehaviour
{
	[SerializeField] private HudId m_id;
	public HudId ID {get { return m_id; }}

	protected void OnEnable ()
	{
		UIFlowManager.onActivateHud += OnActivateHud;
		UIFlowManager.onDeactivateHud += OnDeactivateHud;
	}
	
	protected void OnDisable ()
	{
		UIFlowManager.onActivateHud -= OnActivateHud;
		UIFlowManager.onDeactivateHud -= OnDeactivateHud;
	}
	
	protected virtual void OnActivateHud (HudId p_hudID)
	{
		
	}
	
	protected void OnDeactivateHud (HudId p_newHudID)
	{
		if ((m_id & p_newHudID) <= 0)
		{
			DeactivateHud ();
		}
	}
	
	protected virtual void DeactivateHud ()
	{
		//this.gameObject.SetActive (false);
		Debug.Log ("Deactivate: " + this.GetType().ToString());
	}
}