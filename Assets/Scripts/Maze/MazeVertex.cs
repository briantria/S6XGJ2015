/*
 * developer     : brian g. tria
 * creation date : 2015.11.21
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeVertex : MonoBehaviour
{
    #region Constants
    /*
     *  Connector cases: --, |, L, <, 7, J, T, --|, _|_, |--, +
     *  Case bit representation : 00 00 00
     *      - from leftmost bit : Top neighbor's right wall flag
     *                            Right neighbor's up wall flag
     *                            This wall flag
     *
     *                    Neighbor Up   Neighbor Right     This
     *  Quick reference :   right up  |    right up    | right up
     */
    private const int CONNECTOR_H  = 5; //0x000101; // --
    private const int CONNECTOR_V  = 34;//0x100010; //  |
    private const int CONNECTOR_L0 = 36;//0x100100; //  L
    private const int CONNECTOR_L1 = 6; //0x000110; //  <
    private const int CONNECTOR_L2 = 3; //0x000011; //  7
    private const int CONNECTOR_L3 = 33;//0x100001; //  J
    private const int CONNECTOR_T0 = 7; //0x000111; //  T
    private const int CONNECTOR_T1 = 35;//0x100011; // --|
    private const int CONNECTOR_T2 = 37;//0x100101; // _|_
    private const int CONNECTOR_T3 = 38;//0x100110; // |--
    private const int CONNECTOR_X  = 39;//0x100111; //  +
    
    #endregion

    [SerializeField] private List<GameObject> m_listWalls = new List<GameObject> ();
    [SerializeField] private Transform m_tConnector;
    
    private Dictionary<RelativePosition, GameObject> m_dictWalls = null;
    private SpriteRenderer m_srConnector = null;
    private RelativePosition m_activeWallFlags;
    private Maze m_maze;
    
    private Sprite m_spritePipeL = null;
    private Sprite m_spritePipeI = null;
    private Sprite m_spritePipeX = null;
    private Sprite m_spritePipeT = null;
    
    public int Id { get; set; }
    public IntVector2 Coordinates { get; set; }
    public RelativePosition ActiveWallFlags {get {return m_activeWallFlags;}}
    
    protected void Awake ()
    {
        m_dictWalls = new Dictionary<RelativePosition, GameObject> ()
        {
            { RelativePosition.Up, m_listWalls [0] },
            { RelativePosition.Right, m_listWalls[1] }
        };
        
        m_spritePipeL = Resources.Load<Sprite> ("Images/Pipes/pipes-02");
        m_spritePipeI = Resources.Load<Sprite> ("Images/Pipes/pipes-03");
        m_spritePipeX = Resources.Load<Sprite> ("Images/Pipes/pipes-04");
        m_spritePipeT = Resources.Load<Sprite> ("Images/Pipes/pipes-05");
        
        m_srConnector = m_tConnector.GetComponent<SpriteRenderer> ();
    }
    
    public void ExtendWalls (float p_scale, float p_padding)
    {
        Vector3 scale;
        Vector3 position;
        Transform tTopWall = m_listWalls[0].transform;
        Transform tRightWall = m_listWalls[1].transform;
        
        // adjust wall length
        scale = tTopWall.localScale;
        scale.x *= (p_scale * 1.55f);
        tTopWall.localScale = scale;
        
        scale = tRightWall.localScale;
        scale.x *= (p_scale * 1.55f); // .x because it is rotated
        tRightWall.localScale = scale;
        
        // adjust wall offset
        position = tTopWall.position;
        position.x -= ((1.5f * p_scale * 0.5f)); // sprite_size * ppu * scaleup * 0.5f
        tTopWall.position = position;
        
        position = tRightWall.position;
        position.y -= ((1.5f * p_scale * 0.5f));
        tRightWall.position = position;
    }
    
    public void ConnectorSetup ()
    {
        MazeVertex neighborUp;
        MazeVertex neighborRight;
        int connectorCase = 0;
        int zRotation = 0;
        
        neighborUp = m_maze.GetNeighborOfVertex (this.Id, this.Coordinates, RelativePosition.Up);
        if (neighborUp != null)
        {
            // we're only interested on the upper neighbor's right wall flag
            connectorCase = (int) (neighborUp.ActiveWallFlags & RelativePosition.Right) << 2;
        }
        
        neighborRight = m_maze.GetNeighborOfVertex (this.Id, this.Coordinates, RelativePosition.Right);
        if (neighborRight != null)
        {
            // we're only interested on the right neighbor's up wall flag
            connectorCase = (connectorCase | (int) (neighborRight.ActiveWallFlags & RelativePosition.Up)) << 2;
        }
        else if (neighborUp != null)
        {
            // avoid overwriting neighborRight's value (0x00) with this.ActiveWallFlags
            connectorCase = connectorCase << 2;
        }
        
        connectorCase |= (int) this.ActiveWallFlags;
        string str = "id: " + this.Id + ", coordinate: " + this.Coordinates.ToString () + ", connector: " + connectorCase;
        
        bool bConnectorNeeded = connectorCase > 0;
        m_tConnector.gameObject.SetActive (bConnectorNeeded);
        if (!bConnectorNeeded) { return; };
        
        /*
            private const int CONNECTOR_H  = 0x000101; // --
            private const int CONNECTOR_V  = 0x100010; //  |
            private const int CONNECTOR_L0 = 0x100100; //  L
            private const int CONNECTOR_L1 = 0x000110; //  <
            private const int CONNECTOR_L2 = 0x000011; //  7
            private const int CONNECTOR_L3 = 0x100001; //  J
            private const int CONNECTOR_T0 = 0x000111; //  T
            private const int CONNECTOR_T1 = 0x100011; // --|
            private const int CONNECTOR_T2 = 0x100101; // _|_
            private const int CONNECTOR_T3 = 0x100110; // |--
            private const int CONNECTOR_X  = 0x100111; //  +
         */
        
        switch (connectorCase)
        {
            case CONNECTOR_H: // --
            {
                m_srConnector.sprite = m_spritePipeI;
                zRotation = 0;
                str += ", CONNECTOR_H";
                break;
            }
            
            case CONNECTOR_V: // |
            {
                m_srConnector.sprite = m_spritePipeI;
                zRotation = 90;
                str += ", CONNECTOR_V";
                break;
            }
            
            case CONNECTOR_L0: // L
            {
                m_srConnector.sprite = m_spritePipeL;
                zRotation = 90;
                str += ", CONNECTOR_L0";
                break;
            }
            
            case CONNECTOR_L1: // <
            {
                m_srConnector.sprite = m_spritePipeL;
                zRotation = 0;
                str += ", CONNECTOR_L1";
                break;
            }
            
            case CONNECTOR_L2: // 7
            {
                m_srConnector.sprite = m_spritePipeL;
                zRotation = 270;
                str += ", CONNECTOR_L2";
                break;
            }
            
            case CONNECTOR_L3: // J
            {
                m_srConnector.sprite = m_spritePipeL;
                zRotation = 180;
                str += ", CONNECTOR_L3";
                break;
            }
            
            case CONNECTOR_T1: // --|
            {
                m_srConnector.sprite = m_spritePipeT;
                zRotation = 90;
                str += ", CONNECTOR_T1";
                break;
            }
            
            case CONNECTOR_T2: // _|_
            {
                m_srConnector.sprite = m_spritePipeT;
                zRotation = 0;
                str += ", CONNECTOR_T2";
                break;
            }
            
            case CONNECTOR_T3: // |--
            {
                m_srConnector.sprite = m_spritePipeT;
                zRotation = 270;
                str += ", CONNECTOR_T3";
                break;
            }
                
            case CONNECTOR_T0: // T
            {
                m_srConnector.sprite = m_spritePipeT;
                zRotation = 180;
                str += ", CONNECTOR_T";
                break;
            }
            
            case CONNECTOR_X: // +
            {
                m_srConnector.sprite = m_spritePipeX;
                zRotation = 0;
                str += ", CONNECTOR_X";
                break;
            }
            
            default:
            {
                m_tConnector.gameObject.SetActive (false);
                str += ", DEFAULT";
                break;
            }
        }
        
        m_srConnector.transform.eulerAngles = new Vector3 (0, 0, zRotation);
        Debug.Log (str + ", zRotation: " + zRotation);
    }
    
    public void SetActiveWalls (Maze p_maze, RelativePosition p_activeWallFlags)
    {
        #if UNITY_EDITOR
        // TODO: create a subclass for editor scripts. see: MazeEditorDisplay.cs
        if (m_dictWalls == null)
        {
            m_dictWalls = new Dictionary<RelativePosition, GameObject> ()
            {
                { RelativePosition.Up, m_listWalls [0] },
                { RelativePosition.Right, m_listWalls[1] }
            };
        }
        
        if (m_spritePipeL == null) { m_spritePipeL = Resources.Load<Sprite> ("Images/Pipes/pipes-02"); }
        if (m_spritePipeI == null) { m_spritePipeI = Resources.Load<Sprite> ("Images/Pipes/pipes-03"); }
        if (m_spritePipeX == null) { m_spritePipeX = Resources.Load<Sprite> ("Images/Pipes/pipes-04"); }
        if (m_spritePipeT == null) { m_spritePipeT = Resources.Load<Sprite> ("Images/Pipes/pipes-05"); }
        
        if (m_srConnector == null) { m_srConnector = m_tConnector.GetComponent<SpriteRenderer> (); }
        #endif
    
        m_maze = p_maze;
        m_activeWallFlags = p_activeWallFlags;
        foreach (KeyValuePair<RelativePosition, GameObject> wall in m_dictWalls)
        {
            wall.Value.SetActive ((wall.Key & p_activeWallFlags) > 0);
        }
    }
}