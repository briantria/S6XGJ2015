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
    protected List<WallPlacement> [] m_wallPlacements;

	public void Display (IntVector2 p_iv2Dimension)
    {
        // TODO: Object pooling
        // TODO: cache references
        float padding = 130.0f * Constants.PPU;
        
        for (int iRow = 0; iRow < p_iv2Dimension.y; ++iRow)
        {
            for (int iCol = 0; iCol < p_iv2Dimension.x; ++iCol)
            {
                Transform tVertex = Instantiate<Transform> (Resources.Load<Transform> ("Prefabs/MazeVertex"));
                tVertex.SetParent (this.transform);
                tVertex.position = new Vector3 (padding * iCol, padding * iRow, 0.0f);
                
                MazeVertex vertex = tVertex.GetComponent <MazeVertex>();
                vertex.Id = (p_iv2Dimension.x * iRow) + iCol;
                vertex.Coordinates = new IntVector2 (iCol, iRow);
            }
        }
        
        MazeVertex[] verteces = this.transform.GetComponentsInChildren<MazeVertex> ();
        //Debug.Log ("m_wallPlacements length: " + m_wallPlacements.Length);
        for (int idx = verteces.Length-2; idx >= 0; --idx)
        {
            WallPlacement activeWallFlags = WallPlacement.None;
            int iVertexId = verteces[idx].Id;
            //Debug.Log ("Vertex ID: " + iVertexId);
            
            for (int jdx = m_wallPlacements[iVertexId].Count-1; jdx >= 0; --jdx)
            {
                activeWallFlags |= m_wallPlacements[iVertexId][jdx];
            }
            
            verteces[idx].SetActiveWalls (activeWallFlags);
        }
    }
}

[System.Flags]
public enum WallPlacement
{
    None  = 0,
    Left  = 1 << 0,
    Up    = 1 << 1,
    Right = 1 << 2,
    Down  = 1 << 3
}