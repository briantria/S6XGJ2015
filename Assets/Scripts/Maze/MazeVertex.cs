/*
 * developer     : brian g. tria
 * creation date : 2015.11.21
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeVertex : MonoBehaviour, ISpriteButtonListener
{
    [SerializeField] private List<GameObject> m_listWalls = new List<GameObject> ();
    [SerializeField] private VertexConnector m_vertexConnector;
    [SerializeField] private HexButtonManager m_hexButtonManager;
    
    private Dictionary<RelativePosition, GameObject> m_dictWalls = null;
    private RelativePosition m_activeWallFlags;
    private Maze m_maze;
    
    public int Id { get; set; }
    public IntVector2 Coordinates { get; set; }
    public RelativePosition ActiveWallFlags {get {return m_activeWallFlags;}}
    
//	protected void OnEnable ()
//	{
//		GameManager.OnGamePhaseUpdate += OnGamePhaseUpdate;
//	}
//	
//	protected void OnDisable ()
//	{
//		GameManager.OnGamePhaseUpdate -= OnGamePhaseUpdate;
//	}
    
    protected void Awake ()
    {
        m_dictWalls = new Dictionary<RelativePosition, GameObject> ()
        {
            { RelativePosition.Up, m_listWalls [0] },
            { RelativePosition.Right, m_listWalls[1] }
        };
    }
    
//	private void OnGamePhaseUpdate (GamePhase p_gamePhase)
//	{
//		m_HexButtonManager.gameObject.SetActive (p_gamePhase == GamePhase.Edit);
//	}
    
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
        
        m_hexButtonManager.OnExtendWalls (p_scale);
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
//        string str = "id: " + this.Id + ", coordinate: " + this.Coordinates.ToString () + ", connector: " + connectorCase;
        
        bool bConnectorNeeded = connectorCase > 0;
        m_vertexConnector.Enable (bConnectorNeeded);
        if (bConnectorNeeded) { m_vertexConnector.Setup (connectorCase); }
    }
    
    public void SetActiveWalls (Maze p_maze, RelativePosition p_activeWallFlags)
    {
//        #if UNITY_EDITOR
//        // TODO: create a subclass for editor scripts. see: MazeEditorDisplay.cs
//        if (m_dictWalls == null)
//        {
//            m_dictWalls = new Dictionary<RelativePosition, GameObject> ()
//            {
//                { RelativePosition.Up, m_listWalls [0] },
//                { RelativePosition.Right, m_listWalls[1] }
//            };
//        }
//        
//        if (m_spritePipeL == null) { m_spritePipeL = Resources.Load<Sprite> ("Images/Pipes/pipes-02"); }
//        if (m_spritePipeI == null) { m_spritePipeI = Resources.Load<Sprite> ("Images/Pipes/pipes-03"); }
//        if (m_spritePipeX == null) { m_spritePipeX = Resources.Load<Sprite> ("Images/Pipes/pipes-04"); }
//        if (m_spritePipeT == null) { m_spritePipeT = Resources.Load<Sprite> ("Images/Pipes/pipes-05"); }
//        
//        if (m_srConnector == null) { m_srConnector = m_tConnector.GetComponent<SpriteRenderer> (); }
//        #endif
    
        m_maze = p_maze;
        m_activeWallFlags = p_activeWallFlags;
        foreach (KeyValuePair<RelativePosition, GameObject> wall in m_dictWalls)
        {
            wall.Value.SetActive ((wall.Key & p_activeWallFlags) > 0);
        }
    }

    public void OnClickSpriteButton ()
    {
        //throw new System.NotImplementedException ();
        HexSetupPanel.Instance.Open ();
        HexSetupPanel.Instance.SetHexSetupListener (m_hexButtonManager);
    }
}