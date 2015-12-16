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
	[SerializeField] private GameObject m_objDrowxy;
	[SerializeField] private GameObject m_objXhy;
	[SerializeField] private GameObject m_objGeexy;
	[SerializeField] private GameObject m_objXauxy;
	[SerializeField] private GameObject m_objFlexy;
	[SerializeField] private GameObject m_objQuirxy;

	private static PlayerController m_instance = null;
	public	static PlayerController Instance {get {return m_instance;}}

    public MazeVertex InitMazeVertex { set; get; }

    private Vector3 m_v3InitPosition;
    private Transform m_transform;
	private PlayerType m_currentPlayerCombo = PlayerType.None;
	private Dictionary <PlayerType, GameObject> m_dictPlayerHex = new Dictionary <PlayerType, GameObject> ();
	
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
	
		m_dictPlayerHex.Add (PlayerType.Drowxy, m_objDrowxy);
		m_dictPlayerHex.Add (PlayerType.Xhy,    m_objXhy   );
		m_dictPlayerHex.Add (PlayerType.Geexy,  m_objGeexy );
		m_dictPlayerHex.Add (PlayerType.Xauxy,  m_objXauxy );
		m_dictPlayerHex.Add (PlayerType.Flexy,  m_objFlexy );
		m_dictPlayerHex.Add (PlayerType.Quirxy, m_objQuirxy);
		
		UpdatePlayerComboDisplay ();
	}
	
	private void UpdatePlayerComboDisplay ()
	{
		foreach (KeyValuePair <PlayerType, GameObject> keyValPair in m_dictPlayerHex)
		{
			keyValPair.Value.SetActive ((m_currentPlayerCombo & keyValPair.Key) > 0);
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
            
            break;
        }
        case GamePhase.Edit:
        {
            m_transform.position = m_v3InitPosition;
            m_currentPlayerCombo = PlayerType.None;
            
            break;
        }}
        
        UpdatePlayerComboDisplay ();
    }
    
	public void AddPlayerHex (PlayerType p_playerType)
	{
		m_currentPlayerCombo |= p_playerType;
        UpdatePlayerComboDisplay ();
	}
	
	public void RemovePlayerHex (PlayerType p_playerType)
	{
		m_currentPlayerCombo &= ~p_playerType;
        UpdatePlayerComboDisplay ();
	}
    
    public bool IsInitMazeVertexValid ()
    {
        if (InitMazeVertex == null) { return false; }
        return (InitMazeVertex.PlayerType | PlayerType.None) > 0;
    }
}