/*
 * developer     : brian g. tria
 * creation date : 2015.12.17
 *
 */

using UnityEngine;
using System.Collections;

public class PlayerStateInfo : MonoBehaviour
{
	public PlayerState CurrentPlayerState { set; get; }
    
    private GameObject m_gameObject;
    
    protected void Awake ()
    {
        m_gameObject = this.gameObject;
        CurrentPlayerState = PlayerState.Active;
    }
    
    public void SetActive (bool p_bIsActive)
    {
        m_gameObject.SetActive (p_bIsActive);
    }
}

[System.Flags]
public enum PlayerState
{
    Drained = 0,
    Active  = 1 << 0,
    Asleep  = 1 << 1
}