/*
 * developer     : brian g. tria
 * creation date : 2015.12.17
 *
 */

using UnityEngine;
using System.Collections;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 0.2f;

	protected void Update ()
    {
        #region Movement Control
        if (PlayerController.Instance.IsHorizontalMovementEnabled ())
        {
            Vector3 playerPosition = PlayerController.Instance.transform.position;
        
            if (Input.GetKey (KeyCode.RightArrow))
            {
                playerPosition.x += m_moveSpeed;
            }
            
            if (Input.GetKey (KeyCode.LeftArrow))
            {
                playerPosition.x -= m_moveSpeed;
            }
            
            PlayerController.Instance.transform.position = playerPosition;
        }
        
        if (PlayerController.Instance.IsVerticalMovementEnabled ())
        {
            Vector3 playerPosition = PlayerController.Instance.transform.position;
        
            if (Input.GetKey (KeyCode.UpArrow))
            {
                playerPosition.y += m_moveSpeed;
            }
            
            if (Input.GetKey (KeyCode.DownArrow))
            {
                playerPosition.y -= m_moveSpeed;
            }
            
            PlayerController.Instance.transform.position = playerPosition;
        }
        #endregion
        
        #region Toggle Special Abilities
        if (Input.GetKeyUp (KeyCode.Q))
        {
            // toggle flexy
            // use playerstateinfo
        }
        
        if (Input.GetKeyUp (KeyCode.W))
        {
            // toggle drowxy
        }
        
        if (Input.GetKeyUp (KeyCode.E))
        {
            // toggle quirxy
        }
        
        if (Input.GetKeyUp (KeyCode.A))
        {
            // toggle geexy
        }
        
        if (Input.GetKeyUp (KeyCode.S))
        {
            // toggle xhy
        }
        
        if (Input.GetKeyUp (KeyCode.D))
        {
            // toggle xauxy
        }
        #endregion
    }
}