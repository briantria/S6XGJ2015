/*
 * developer     : brian g. tria
 * creation date : 2015.12.17
 *
 */

using UnityEngine;
using System.Collections;

public class PlayerStateInfo : MonoBehaviour
{
    private const float BATTERY_LIFE = 1.0f;
    
    [SerializeField] private SpriteRenderer m_spriteRendererFace;
    
	public PlayerState CurrentPlayerState { set; get; }
    
    private GameObject m_gameObject;
    private float m_fBatteryLifeLeft;
    
    protected void Awake ()
    {
        m_gameObject = this.gameObject;
        CurrentPlayerState = PlayerState.Active;
    }
    
    private IEnumerator BatteryDrain ()
    {
        while (m_fBatteryLifeLeft > BATTERY_LIFE * 0.3f)
        {
            m_fBatteryLifeLeft -= 0.01f;
            yield return new WaitForSeconds (0.1f);
        }
    
        while (m_fBatteryLifeLeft > 0)
        {
            m_fBatteryLifeLeft -= 0.01f;
            m_spriteRendererFace.color = new Color (1.0f, 1.0f, 1.0f, Mathf.PingPong (Time.time, 0.4f) + 0.6f);
            yield return new WaitForSeconds (0.1f);
        }
        
        m_spriteRendererFace.color = new Color (0.6f, 0.6f, 0.6f, 1.0f);
        CurrentPlayerState = PlayerState.Drained;
        yield return new WaitForEndOfFrame ();
    }
    
    public void SetActive (bool p_bIsActive)
    {
        m_gameObject.SetActive (p_bIsActive);
    }
    
    public void ResetBattery ()
    {
        m_fBatteryLifeLeft = BATTERY_LIFE;
        CurrentPlayerState = PlayerState.Active;
        m_spriteRendererFace.color = Color.white;
        StopCoroutine ("BatteryDrain");
        StartCoroutine ("BatteryDrain");
    }
    
    public void ToggleActiveSleepState ()
    {
        if (CurrentPlayerState == PlayerState.Drained) { return; }
        
        if (CurrentPlayerState == PlayerState.Active)
        {
            CurrentPlayerState = PlayerState.Asleep;
            m_spriteRendererFace.color = new Color (1.0f, 1.0f, 1.0f, 0.6f);
            StopCoroutine ("BatteryDrain");
        }
        else if (CurrentPlayerState == PlayerState.Asleep)
        {
            CurrentPlayerState = PlayerState.Active;
            m_spriteRendererFace.color = Color.white;
            StopCoroutine ("BatteryDrain");
            StartCoroutine ("BatteryDrain");
        }
    }
}

[System.Flags]
public enum PlayerState
{
    Drained = 0,
    Active  = 1 << 0,
    Asleep  = 1 << 1
}