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
	[SerializeField] private Text m_textPlayButtonLabel;
	
	public void ToggleGamePhase ()
	{
		if (GameManager.Instance.CurrentGamePhase == GamePhase.Edit)
		{
            if (PlayerController.Instance.IsInitMazeVertexValid ())
            {   
                m_textPlayButtonLabel.text = "Edit";
                GameManager.Instance.UpdateGamePhase (GamePhase.Play);
            }
            else 
            {
                Debug.LogError ("Invalid Init Maze Vertex!");
            }
		}
		else if (GameManager.Instance.CurrentGamePhase == GamePhase.Play)
		{
			m_textPlayButtonLabel.text = "Play";
			GameManager.Instance.UpdateGamePhase (GamePhase.Edit);
		}
	}
}