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
	private Dictionary <PlayerType, Sprite> m_dictFaces = new Dictionary <PlayerType, Sprite> ();
    
    private Sprite m_spriteBody;
    private Sprite m_spriteFaceDrowxy;
    private Sprite m_spriteFaceGeexy;
    private Sprite m_spriteFaceXhy;
	private Sprite m_spriteFaceXauxy;
	private Sprite m_spriteFaceFlexy;
	private Sprite m_spriteFaceQuirxy;
    
    #region PROPERTIES
    public Dictionary <PlayerType, Color> PlayerColor {get {return m_dictColors;}}
	public Dictionary <PlayerType, Sprite> PlayerFace {get {return m_dictFaces;}}
    
    public Sprite SpriteBody       {get {return m_spriteBody;}}
	public Sprite SpriteFaceDrowxy {get {return m_spriteFaceDrowxy;}}
	public Sprite SpriteFaceGeexy  {get {return m_spriteFaceGeexy;}}
	public Sprite SpriteFaceXhy    {get {return m_spriteFaceXhy;}}
	public Sprite SpriteFaceXauxy  {get {return m_spriteFaceXauxy;}}
	public Sprite SpriteFaceFlexy  {get {return m_spriteFaceFlexy;}}
	public Sprite SpriteFaceQuirxy {get {return m_spriteFaceQuirxy;}}
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
    	// use dictionary
    
        m_spriteBody = Resources.Load<Sprite> ("Images/Player/body");
		m_spriteFaceDrowxy = Resources.Load<Sprite> ("Images/Player/face_blue_drowxy");
		m_spriteFaceGeexy = Resources.Load<Sprite> ("Images/Player/face_green_geexy");
		m_spriteFaceXhy = Resources.Load<Sprite> ("Images/Player/face_mint_green_xhy");
		m_spriteFaceXauxy = Resources.Load<Sprite> ("Images/Player/face_pink_xauxy");
		m_spriteFaceFlexy = Resources.Load<Sprite> ("Images/Player/face_red_flexy");
		m_spriteFaceQuirxy = Resources.Load<Sprite> ("Images/Player/face_yellow_quirxy");
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