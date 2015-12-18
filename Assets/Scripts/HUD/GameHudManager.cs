/*
 * developer     : brian g. tria
 * creation date : 2015.10.28
 *
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHudManager : MonoBehaviour //HudManager
{
	[SerializeField] private Text m_textCurrentPhase;
	[SerializeField] private Text m_textPlayButtonLabel;
	
	protected void OnEnable ()
	{
		GameManager.OnGamePhaseUpdate += OnGamePhaseUpdate;
	}
	
	protected void OnDisable ()
	{
		GameManager.OnGamePhaseUpdate -= OnGamePhaseUpdate;
	}
	
	public void ToggleGamePhase ()
	{
        HexSetupPanel.Instance.Close ();
        ResultScreenManager.Instance.Close ();
        GameManager.OnPause = false;
    
		if (GameManager.Instance.CurrentGamePhase == GamePhase.Edit)
		{
            if (PlayerController.Instance.IsInitMazeVertexValid ())
            {   
                GameManager.Instance.UpdateGamePhase (GamePhase.Play);
            }
            else 
            {
                Debug.LogError ("Invalid Init Maze Vertex!");
            }
		}
		else if (GameManager.Instance.CurrentGamePhase == GamePhase.Play)
		{
			GameManager.Instance.UpdateGamePhase (GamePhase.Edit);
		}
	}
	
	public void OnGamePhaseUpdate (GamePhase p_gamePhase)
	{
		switch (p_gamePhase) {
		case GamePhase.Edit:
		{
			m_textCurrentPhase.text = "Edit Phase";
			m_textPlayButtonLabel.text = "Play";
			break;
		}
		case GamePhase.Play:
		{
			m_textCurrentPhase.text = "Play Phase";
			m_textPlayButtonLabel.text = "Edit";
			break;
		}}
	}
	
	public void OpenTutorial ()
	{
		TutorialScreenManager.Instance.Open ();
	}
}