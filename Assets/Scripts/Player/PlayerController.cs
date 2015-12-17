/*
 * developer     : brian g. tria
 * creation date : 2015.11.30
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private GameObject  m_objDrowxy;
	[SerializeField] private GameObject  m_objXhy;
	[SerializeField] private GameObject  m_objGeexy;
	[SerializeField] private GameObject  m_objXauxy;
	[SerializeField] private GameObject  m_objFlexy;
	[SerializeField] private GameObject  m_objQuirxy;

	private static PlayerController m_instance = null;
	public	static PlayerController Instance {get {return m_instance;}}
	
	public delegate void ComboAction (PlayerType p_playerType);
	public static event ComboAction OnPlayerComboUpdate;

    public MazeVertex InitMazeVertex { set; get; }
    public PlayerType CurrentPlayerType {get {return m_currentPlayerCombo;}}

    private Vector3 m_v3InitPosition;
    private Transform m_transform;
    private Rigidbody2D m_rigidBody2D;
    private PlayerType m_currentPlayerCombo = PlayerType.None;
    private Dictionary <PlayerType, PlayerStateInfo> m_dictPlayerStateInfo = new Dictionary <PlayerType, PlayerStateInfo> ();
	
    #region Initializations
    protected void OnEnable ()
    {
        GameManager.OnGamePhaseUpdate += OnGamePhaseUpdate;
    }
    
    protected void OnDisable ()
    {
        GameManager.OnGamePhaseUpdate -= OnGamePhaseUpdate;
    }
    
	protected void Awake ()
	{
		m_instance = this;
        m_transform = this.transform;
        m_v3InitPosition = m_transform.position;
        m_rigidBody2D = this.GetComponent<Rigidbody2D> ();
        
        m_dictPlayerStateInfo.Add (PlayerType.Drowxy, m_objDrowxy.GetComponent<PlayerStateInfo> ());
        m_dictPlayerStateInfo.Add (PlayerType.Xhy,    m_objXhy   .GetComponent<PlayerStateInfo> ());
        m_dictPlayerStateInfo.Add (PlayerType.Geexy,  m_objGeexy .GetComponent<PlayerStateInfo> ());
        m_dictPlayerStateInfo.Add (PlayerType.Xauxy,  m_objXauxy .GetComponent<PlayerStateInfo> ());
        m_dictPlayerStateInfo.Add (PlayerType.Flexy,  m_objFlexy .GetComponent<PlayerStateInfo> ());
        m_dictPlayerStateInfo.Add (PlayerType.Quirxy, m_objQuirxy.GetComponent<PlayerStateInfo> ());
		
		UpdatePlayerComboDisplay ();
	}
    #endregion
	
    #region Game Loop Updates / Events
	private void UpdatePlayerComboDisplay ()
	{
        foreach (KeyValuePair <PlayerType, PlayerStateInfo> keyValPair in m_dictPlayerStateInfo)
		{
			keyValPair.Value.SetActive ((m_currentPlayerCombo & keyValPair.Key) > 0);
		}
        
        if ((m_currentPlayerCombo & PlayerType.Drowxy) > 0)
        {
            m_rigidBody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            m_rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        
        if (OnPlayerComboUpdate != null)
        {
        	OnPlayerComboUpdate (m_currentPlayerCombo);
        }
	}
	
    private void OnGamePhaseUpdate (GamePhase p_gamePhase)
    {
        switch (p_gamePhase){
        case GamePhase.Play:
        {
            Vector3 position = m_transform.position;
            position.x = InitMazeVertex.transform.position.x;
            position.y = InitMazeVertex.transform.position.y;
            m_transform.position = position;
            m_currentPlayerCombo = InitMazeVertex.PlayerType;
            m_rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            InitMazeVertex.AnimateOut ();
            
            break;
        }
        case GamePhase.Edit:
        {
            m_rigidBody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            m_transform.position = m_v3InitPosition;
            m_currentPlayerCombo = PlayerType.None;
            
            break;
        }}
        
        UpdatePlayerComboDisplay ();
    }
    #endregion
    
	public void AddPlayerHex (PlayerType p_playerType)
	{
		m_currentPlayerCombo |= p_playerType;
        UpdatePlayerComboDisplay ();
        m_dictPlayerStateInfo[p_playerType].ResetBattery ();
	}
	
	public void RemovePlayerHex (PlayerType p_playerType)
	{
		m_currentPlayerCombo &= ~p_playerType;
        UpdatePlayerComboDisplay ();
	}
    
    public PlayerState GetPlayerState (PlayerType p_playerType)
    {
        return m_dictPlayerStateInfo[p_playerType].CurrentPlayerState;
    }
    
    public void ToggleActiveAsleepState (PlayerType p_playerType)
    {
        m_dictPlayerStateInfo[p_playerType].ToggleActiveSleepState ();
    }
    
    #region Boolean Checkers
    public bool IsHorizontalMovementEnabled ()
    {
        PlayerState state = PlayerState.Drained;
        
        foreach (KeyValuePair <PlayerType, PlayerStateInfo> pair in m_dictPlayerStateInfo)
        {
            state |= pair.Value.CurrentPlayerState;
        }
        
        bool enable =  (m_currentPlayerCombo | PlayerType.None) > 0
                    && (state & PlayerState.Active) > 0;
        
        return enable;
    }
    
    public bool IsVerticalMovementEnabled ()
    {
        bool enable =  (m_currentPlayerCombo & PlayerType.Drowxy) > 0
                    && (m_dictPlayerStateInfo[PlayerType.Drowxy].CurrentPlayerState & PlayerState.Active) > 0;
        
        if (enable)
        {
            m_rigidBody2D.constraints = RigidbodyConstraints2D.FreezeAll;    
        }
        else
        {
            m_rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        
        return enable;
    }
    
    public bool IsInitMazeVertexValid ()
    {
        if (InitMazeVertex == null) { return false; }
        return (InitMazeVertex.PlayerType | PlayerType.None) > 0;
    }
    #endregion
}