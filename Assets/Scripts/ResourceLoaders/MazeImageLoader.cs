/*
 * developer     : brian g. tria
 * creation date : 2015.11.30
 *
 */

using UnityEngine;
using System.Collections;

public class MazeImageLoader : ScriptableObject
{
    private Sprite m_spritePipeL;
    private Sprite m_spritePipeI;
    private Sprite m_spritePipeX;
    private Sprite m_spritePipeT;
    
    private Sprite m_spriteHexButton;

    #region SINGLETON
	private static MazeImageLoader m_instance = null;
    public  static MazeImageLoader Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = ScriptableObject.CreateInstance<MazeImageLoader> ();
            }
        
            return m_instance;
        }
    }
    #endregion
    
    #region PROPERTIES
    public Sprite PipeL {get {return m_spritePipeL;}}
    public Sprite PipeI {get {return m_spritePipeI;}}
    public Sprite PipeX {get {return m_spritePipeX;}}
    public Sprite PipeT {get {return m_spritePipeT;}}
    
    public Sprite SpriteHexButton {get {return m_spriteHexButton;}}
    #endregion
    
    public void LoadImages ()
    {
        m_spritePipeL = Resources.Load<Sprite> ("Images/Pipes/pipes-02");
        m_spritePipeI = Resources.Load<Sprite> ("Images/Pipes/pipes-03");
        m_spritePipeX = Resources.Load<Sprite> ("Images/Pipes/pipes-04");
        m_spritePipeT = Resources.Load<Sprite> ("Images/Pipes/pipes-05");
        
        m_spriteHexButton = Resources.Load<Sprite> ("Images/hex_outline_glow");
    }
}