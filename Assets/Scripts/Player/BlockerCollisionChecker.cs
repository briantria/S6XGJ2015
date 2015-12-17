/*
 * developer     : brian g. tria
 * creation date : 2015.12.17
 *
 */

using UnityEngine;
using System.Collections;

public class BlockerCollisionChecker : MonoBehaviour
{
    public static BlockerPosition CurrentBlockers { set; get; }

//    protected void OnCollisionExit2D (Collision2D p_collision)
//    {
//        CurrentBlockers = BlockerPosition.None;
//    }

    protected void OnCollisionEnter2D (Collision2D p_collision)
    {
        // TEMPORARY FIX: PLAYER SHOULD AVOID WALLS!
        // PROBLEM WITH COLLISION DETECTION
        
        if (p_collision.gameObject.CompareTag ("EndPoint"))
        {
            // success
            //Debug.Log ("WIN");
            GameManager.OnPause = true;
            ResultScreenManager.Instance.SetResults (ResultEnum.Win);
            ResultScreenManager.Instance.Open ();
        }
        else
        {
			GameManager.OnPause = true;
			ResultScreenManager.Instance.SetResults (ResultEnum.Lose);
			ResultScreenManager.Instance.Open ();
            // game over
        }
        
        //Debug.LogError ("Game Over!");
    }

//    protected void OnCollisionStay2D (Collision2D p_collision)
//    {
//        //Debug.Log ("name: " + p_collision.gameObject.name);
//        Collider2D collider = p_collision.collider;
//        Vector3 center = collider.bounds.center;
//        ContactPoint2D[] collisionContacts = p_collision.contacts;
//        
//        CurrentBlockers = BlockerPosition.None;
//        
////        for (int idx = collisionContacts.Length - 1; idx >= 0; --idx)
////        {
//            Vector3 collisionPoint = collisionContacts[0].point;
//        
//            if (collisionPoint.x < center.x)
//            {
//                CurrentBlockers |= BlockerPosition.Left;
//            }
//            
//            if (collisionPoint.x > center.x)
//            {
//                CurrentBlockers |= BlockerPosition.Right;
//            }
//            
//            if (collisionPoint.y < center.y)
//            {
//                CurrentBlockers |= BlockerPosition.Down;
//            }
//            
//            if (collisionPoint.y > center.y)
//            {
//                CurrentBlockers |= BlockerPosition.Up;
//            }
//            
////            Debug.Log (CurrentBlockers);
////        }
//    }
}

[System.Flags]
public enum BlockerPosition
{
    None  = 0,
    Up    = 1 << 0,
    Right = 1 << 1,
    Down  = 1 << 2,
    Left  = 1 << 3
}