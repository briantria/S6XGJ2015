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

	private PlayerType m_currentPlayerCombo = PlayerType.None;
	private Dictionary <PlayerType, GameObject> m_dictPlayerHex = new Dictionary <PlayerType, GameObject> ();
	
	protected void Awake ()
	{
		m_instance = this;
	
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
	
	public void AddPlayerHex (PlayerType p_playerType)
	{
		m_currentPlayerCombo |= p_playerType;
	}
	
	public void RemovePlayerHex (PlayerType p_playerType)
	{
		m_currentPlayerCombo &= ~p_playerType;
	}
}