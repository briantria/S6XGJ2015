/*
 * developer     : brian g. tria
 * creation date : 2015.11.23
 *
 */

using UnityEngine;
using System.Collections;

public class SpriteButton : MonoBehaviour
{
    [SerializeField] private GameObject m_objSpriteButtonListener;
    private ISpriteButtonListener m_spriteButtonListener;

    protected void Awake ()
    {
        m_spriteButtonListener = m_objSpriteButtonListener.GetComponent<ISpriteButtonListener> ();
    }

    protected void OnMouseUpAsButton ()
    {
        if (InputManager.OverlapClick () > 1)
        {
            return;
        }
        
        m_spriteButtonListener.OnClickSpriteButton ();
    }
}