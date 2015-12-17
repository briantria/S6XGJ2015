/*
 * developer     : brian g. tria
 * creation date : 2015.12.17
 *
 */

using UnityEngine;
using System.Collections;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 0.1f;

	protected void Update ()
    {
    	if (GameManager.OnPause) {return;}
    
        #region Movement Control
        if (PlayerController.Instance.IsHorizontalMovementEnabled ())
        {
            Vector3 playerPosition = PlayerController.Instance.transform.position;
        
            if (Input.GetKey (KeyCode.RightArrow))
//            && (BlockerCollisionChecker.CurrentBlockers & BlockerPosition.Right) <= 0)
            {
                playerPosition.x += m_moveSpeed;
            }
            
            if (Input.GetKey (KeyCode.LeftArrow))
//            && (BlockerCollisionChecker.CurrentBlockers & BlockerPosition.Left) <= 0)
            {
                playerPosition.x -= m_moveSpeed;
            }
            
            PlayerController.Instance.transform.position = playerPosition;
//            BlockerCollisionChecker.CurrentBlockers = BlockerPosition.None;
        }
        
        if (PlayerController.Instance.IsVerticalMovementEnabled ())
        {
            Vector3 playerPosition = PlayerController.Instance.transform.position;
        
            if (Input.GetKey (KeyCode.UpArrow))
//            && (BlockerCollisionChecker.CurrentBlockers & BlockerPosition.Up) <= 0)
            {
                playerPosition.y += m_moveSpeed;
            }
            
            if (Input.GetKey (KeyCode.DownArrow))
//            && (BlockerCollisionChecker.CurrentBlockers & BlockerPosition.Down) <= 0)
            {
                playerPosition.y -= m_moveSpeed;
            }
            
            PlayerController.Instance.transform.position = playerPosition;
//            BlockerCollisionChecker.CurrentBlockers = BlockerPosition.None;
        }
        #endregion
        
        #region Toggle Special Abilities
        if (Input.GetKeyUp (KeyCode.Q))
        {
            PlayerController.Instance.ToggleActiveAsleepState (PlayerType.Flexy);
        }
        
        if (Input.GetKeyUp (KeyCode.W))
        {
            PlayerController.Instance.ToggleActiveAsleepState (PlayerType.Drowxy);
        }
        
        if (Input.GetKeyUp (KeyCode.E))
        {
            PlayerController.Instance.ToggleActiveAsleepState (PlayerType.Quirxy);
        }
        
        if (Input.GetKeyUp (KeyCode.A))
        {
            PlayerController.Instance.ToggleActiveAsleepState (PlayerType.Geexy);
        }
        
        if (Input.GetKeyUp (KeyCode.S))
        {
            PlayerController.Instance.ToggleActiveAsleepState (PlayerType.Xhy);
        }
        
        if (Input.GetKeyUp (KeyCode.D))
        {
            PlayerController.Instance.ToggleActiveAsleepState (PlayerType.Xauxy);
        }
        #endregion
    }
}