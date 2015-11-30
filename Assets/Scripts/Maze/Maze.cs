/*
 * developer     : brian g. tria
 * creation date : 2015.11.20
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour
{
    private readonly float PADDING = Constants.PPU * 280.0f;
    private readonly float OFFSET  = Constants.PPU * 600.0f;
    
    [SerializeField] private Transform m_tVertexContainer = null;

    protected List<RelativePosition> [] m_wallPlacements;
    private   List<MazeVertex> m_listVerteces = new List<MazeVertex> ();
    private   Camera m_mainCamera;
    private   int m_iWidth;
    private   int m_iHeight;
    
    private Transform m_tStartPoint;
    private Transform m_tEndPoint;
    // for demo, set init value
    private   int m_iStartPointID = 2;
    private   int m_iEndPointID = 3;

	protected void OnEnable ()
	{
		GameManager.OnGamePhaseUpdate += OnGamePhaseUpdate;
        AppFlowManager.appStateUpdate += AppStateUpdate;
	}
	
	protected void OnDisable ()
	{
        GameManager.OnGamePhaseUpdate -= OnGamePhaseUpdate;
        AppFlowManager.appStateUpdate -= AppStateUpdate;
	}

    protected void Awake ()
    {
        m_mainCamera = Camera.main;
        m_tStartPoint = Instantiate<Transform> (Resources.Load<Transform> ("Prefabs/StartEndPoint"));// new GameObject ("StartPoint").transform;
        m_tEndPoint = Instantiate<Transform> (Resources.Load<Transform> ("Prefabs/StartEndPoint"));//new GameObject ("EndPoint").transform;
        
        m_tStartPoint.SetParent (this.transform);
        m_tEndPoint.SetParent (this.transform);
        
        m_tStartPoint.name = "StartPoint";
        m_tEndPoint.name = "EndPoint";
    }
    
    private void OnGamePhaseUpdate (GamePhase p_gamePhase)
    {
        switch (p_gamePhase)
        {
//            case GamePhase.Edit:
//            {
//                MazeGenerator.Clear ();
//                MazeGenerator.Create (30, 20);
//                break;
//            }
            case GamePhase.Play:
            {
                HexSetupPanel.Instance.Close ();
                Vector3 pos = m_tStartPoint.position;
                pos.x +=   7;
                pos.y +=   4;
                pos.z  = -10;
                m_mainCamera.transform.position = pos;
                m_mainCamera.orthographicSize = CameraZoom.ORTHO_SIZE;
                
                break;
            }
        }
    }
    
    private void AppStateUpdate (AppState p_appState)
    {
        switch (p_appState)
        {
            case AppState.OnGameScreen:
            {
                HexSetupPanel.Instance.Close ();
                MazeGenerator.Clear ();
                MazeGenerator.Create (20, 10);
                
                float scaleUp = 2.5f;
                for (int idx = 0; idx < m_listVerteces.Count; ++idx)
                {
                    m_listVerteces[idx].transform.position = m_listVerteces[idx].transform.position * scaleUp;
                    m_listVerteces[idx].ExtendWalls (scaleUp, PADDING);
                    
                    if (idx == m_iStartPointID)
                    {
                        Vector3 pos = m_listVerteces[idx].transform.position;
                        pos.x -= (PADDING * scaleUp * 0.5f);
                        pos.y -= (PADDING * scaleUp * 0.5f);
                        m_tStartPoint.position = pos;
                        
                        m_tStartPoint.name = "StartPoint at " + m_listVerteces[idx].Id;
                    
                        pos.x +=   7;
                        pos.y +=   4;
                        pos.z  = -10;
                        m_mainCamera.transform.position = pos;
                    }
                    
                    if (idx == m_iEndPointID)
                    {
                        Vector3 pos = m_listVerteces[idx].transform.position;
                        pos.x -= (PADDING * scaleUp * 0.5f);
                        pos.y -= (PADDING * scaleUp * 0.5f);
                        m_tEndPoint.position = pos;
                    }
                }
                break;
            }
        }
    }
    
    public void SetVertexContainer (Transform p_container)
    {
        m_tVertexContainer = p_container;
    }
    
    public void InitVertexPool ()
    {
        for (int idx = 0; idx < 600; ++idx)
        {
            Transform tVertex = Instantiate<Transform> (Resources.Load<Transform> ("Prefabs/MazeVertex"));
            tVertex.SetParent (m_tVertexContainer);
            m_listVerteces.Add (tVertex.GetComponent <MazeVertex>());
            tVertex.gameObject.SetActive (false);
        }
    }
    
    public void LoadWallPlacements (List<RelativePosition> [] p_wallPlacements)
    {
        m_wallPlacements = p_wallPlacements;
    }

	public void Display (IntVector2 p_iv2Dimension)
    {
        // TODO: cache references
        if (m_tVertexContainer == null)
        {
            GameObject obj = new GameObject("Verteces");
            m_tVertexContainer = obj.transform;
            m_tVertexContainer.SetParent (this.transform);
        }
        
        m_iWidth = p_iv2Dimension.x;
        m_iHeight = p_iv2Dimension.y;
        
        // TODO: combine initial setup loop, wall setup loop, and connector setup loop
        
        // initial setup loop
        for (int iRow = 0; iRow < p_iv2Dimension.y; ++iRow)
        {
            for (int iCol = 0; iCol < p_iv2Dimension.x; ++iCol)
            {
                int id = (p_iv2Dimension.x * iRow) + iCol;
                Transform tVertex;
                MazeVertex vertex;
                
                if (id < m_listVerteces.Count)
                {
                    tVertex = m_listVerteces[id].transform;
                    tVertex.gameObject.SetActive (true);
                    vertex = tVertex.GetComponent <MazeVertex>();
                }
                else
                {
                    tVertex = Instantiate<Transform> (Resources.Load<Transform> ("Prefabs/MazeVertex"));
                    tVertex.SetParent (m_tVertexContainer);
                    vertex = tVertex.GetComponent <MazeVertex>();
                    m_listVerteces.Add (vertex);
                }
            
                tVertex.position = new Vector3 ((PADDING * iCol) - OFFSET, (PADDING * iRow) - OFFSET, 0.0f);
                
                vertex.Id = id;
                vertex.Coordinates = new IntVector2 (iCol, iRow);
                tVertex.name = "[" + vertex.Id + "]" + " " + vertex.Coordinates.ToString ();
            }
        }
        
        // wall setup loop
        //MazeVertex[] verteces = m_tVertexContainer.GetComponentsInChildren<MazeVertex> ();
        for (int idx = m_listVerteces.Count-2; idx >= 0; --idx)
        {
            RelativePosition activeWallFlags = RelativePosition.None;
            int iVertexId = m_listVerteces[idx].Id;
            
            if (iVertexId < m_wallPlacements.Length)
            {
                for (int jdx = m_wallPlacements[iVertexId].Count-1; jdx >= 0; --jdx)
                {
                    activeWallFlags |= m_wallPlacements[iVertexId][jdx];
                } 
            }
            
            m_listVerteces[idx].SetActiveWalls (this, activeWallFlags);
        }
        
        // connector setup loop
        for (int idx = m_listVerteces.Count-2; idx >= 0; --idx)
        {
            m_listVerteces[idx].ConnectorSetup ();
        }
    }
    
    public MazeVertex GetNeighborOfVertex (int p_id, IntVector2 p_iv2Coordinates, RelativePosition p_relativePosition)
    {
        MazeVertex vertex = null;
        
        switch (p_relativePosition)
        {
            case RelativePosition.Up:
            {
                if (p_iv2Coordinates.y + 1 >= m_iHeight) { break; }
                vertex = m_listVerteces [p_id + m_iWidth];
                
                break;
            }
            
            case RelativePosition.Right:
            {
                if (p_iv2Coordinates.x + 1 >= m_iWidth) { break; }
                vertex = m_listVerteces [p_id + 1];
                
                break;
            }
        }
        
        return vertex;
    }
}

[System.Flags]
public enum RelativePosition
{
    None  = 0,
    Up    = 1 << 0,
    Right = 1 << 1,
//    Down  = 1 << 2,
//    Left  = 1 << 3
}