/*
 * developer     : brian g. tria
 * creation date : 2015.12.01
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerTypeInfo : ScriptableObject
{
    #region SINGLETON
	private static PlayerTypeInfo m_instance = null;
    public  static PlayerTypeInfo Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = ScriptableObject.CreateInstance<PlayerTypeInfo> ();
            }
        
            return m_instance;
        }
    }
    #endregion
    
    private Dictionary <PlayerType, Color> m_dictColors = new Dictionary <PlayerType, Color> ();
    private Sprite m_spriteBody;
    
    #region PROPERTIES
    public Dictionary <PlayerType, Color> PlayerColor {get {return m_dictColors;}}
    public Sprite SpriteBody {get {return m_spriteBody;}}
    #endregion
    
    public void LoadColors ()
    {
        float multiplier = 1.0f / 255.0f;
    
        m_dictColors.Add (PlayerType.None,   new Color (170 * multiplier, 236 * multiplier, 206 * multiplier, 255 * multiplier));
        m_dictColors.Add (PlayerType.Drowxy, new Color (144 * multiplier, 189 * multiplier, 211 * multiplier, 255 * multiplier));
        m_dictColors.Add (PlayerType.Xhy,    new Color (225 * multiplier, 106 * multiplier,  92 * multiplier, 255 * multiplier));
        m_dictColors.Add (PlayerType.Quirxy, new Color (103 * multiplier, 208 * multiplier, 206 * multiplier, 255 * multiplier));
        m_dictColors.Add (PlayerType.Flexy,  new Color (244 * multiplier, 242 * multiplier, 144 * multiplier, 255 * multiplier));
        m_dictColors.Add (PlayerType.Geexy,  new Color (203 * multiplier, 109 * multiplier, 233 * multiplier, 255 * multiplier));
        m_dictColors.Add (PlayerType.Xauxy,  new Color (189 * multiplier, 221 * multiplier,  99 * multiplier, 255 * multiplier));
    }
    
    public void LoadImages ()
    {
        m_spriteBody = Resources.Load<Sprite> ("Images/Player/Body");
    }
}

[System.Flags]
public enum PlayerType
{
    None   = 0,
    Drowxy = 1 << 0,
    Xhy    = 1 << 1,
    Quirxy = 1 << 2,
    Flexy  = 1 << 3,
    Geexy  = 1 << 4,
    Xauxy  = 1 << 5
}