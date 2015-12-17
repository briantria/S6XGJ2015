/*
 * developer     : brian g. tria
 * creation date : 2015.11.22
 *
 */

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private static GameManager m_instance = null;
	public  static GameManager Instance {get {return m_instance;}}
	
	private GamePhase m_currentGamePhase;
	public GamePhase CurrentGamePhase {get {return m_currentGamePhase;}}
	public static bool OnPause { set; get; }
	
	public delegate void GamePhaseAction (GamePhase p_gamePhase);
	public static event GamePhaseAction OnGamePhaseUpdate;
	
	protected void Awake ()
	{
		m_instance = this;
		m_currentGamePhase = GamePhase.Edit;
		OnPause = false;
	}
	
	public void UpdateGamePhase (GamePhase p_gamePhase)
	{
		m_currentGamePhase = p_gamePhase;
	
		if (OnGamePhaseUpdate != null)
		{
			OnGamePhaseUpdate (p_gamePhase);
		}
	}
}

public enum GamePhase
{
	Edit,
	Play
}