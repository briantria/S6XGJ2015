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
    private readonly float PADDING = 280.0f * Constants.PPU;
    private readonly float OFFSET  = 600.0f * Constants.PPU;

    protected List<RelativePosition> [] m_wallPlacements;
    private   List<MazeVertex> m_listVerteces;
    private   int m_iWidth;
    private   int m_iHeight;

    protected void Awake ()
    {
        Debug.Log ("MAZE AWAKE");
    
        // For Demo
        float scaleUp = 2.5f;
        MazeVertex[] verteces = this.transform.GetComponentsInChildren<MazeVertex> ();
        for (int idx = 0; idx < verteces.Length; ++idx)
        {
            verteces[idx].transform.position = verteces[idx].transform.position * scaleUp;
            verteces[idx].ExtendWalls (scaleUp, PADDING);
        }
        ////////////////
    }

	public void Display (IntVector2 p_iv2Dimension)
    {
        // TODO: Object pooling
        // TODO: cache references
        m_listVerteces = new List<MazeVertex> ();
        m_iWidth = p_iv2Dimension.x;
        m_iHeight = p_iv2Dimension.y;
        
        // TODO: combine initial setup loop, wall setup loop, and connector setup loop
        
        // initial setup loop
        for (int iRow = 0; iRow < p_iv2Dimension.y; ++iRow)
        {
            for (int iCol = 0; iCol < p_iv2Dimension.x; ++iCol)
            {
                Transform tVertex = Instantiate<Transform> (Resources.Load<Transform> ("Prefabs/MazeVertex"));
                tVertex.SetParent (this.transform);
                tVertex.position = new Vector3 ((PADDING * iCol) - OFFSET, (PADDING * iRow) - OFFSET, 0.0f);
                
                MazeVertex vertex = tVertex.GetComponent <MazeVertex>();
                vertex.Id = (p_iv2Dimension.x * iRow) + iCol;
                vertex.Coordinates = new IntVector2 (iCol, iRow);
                m_listVerteces.Add (vertex);
            }
        }
        
        // wall setup loop
        MazeVertex[] verteces = this.transform.GetComponentsInChildren<MazeVertex> ();
        for (int idx = verteces.Length-2; idx >= 0; --idx)
        {
            RelativePosition activeWallFlags = RelativePosition.None;
            int iVertexId = verteces[idx].Id;
            
            for (int jdx = m_wallPlacements[iVertexId].Count-1; jdx >= 0; --jdx)
            {
                activeWallFlags |= m_wallPlacements[iVertexId][jdx];
            }
            
            verteces[idx].SetActiveWalls (this, activeWallFlags);
        }
        
        // connector setup loop
        for (int idx = verteces.Length-2; idx >= 0; --idx)
        {
            verteces[idx].ConnectorSetup ();
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