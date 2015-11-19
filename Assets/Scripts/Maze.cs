/*
 * developer     : brian g. tria
 * creation date : 2015.11.20
 *
 * Given a 2 x 2 maze, we'll get a 4 x 4 adjacency matrix
 *
 *         (0,0) (1,0) (0,1) (1,1)
 *  (0,0) [  x,    0,    0,    0  ]
 *  (1,0) [  x,    x,    0,    0  ]
 *  (0,1) [  x,    x,    x,    0  ]
 *  (1,1) [  x,    x,    x,    x  ]
 *
 * For an undirected graph, we don't need to save those cells marked with 'x'.
 * Our maze can be represented using an undirected graph.
 *
 * Optimized adjacency matrix for undirected graphs
 *
 *         (1,0) (0,1) (1,1)
 *  (0,0) [  0,    0,    0  ]
 *  (1,0) [  x,    0,    0  ]
 *  (0,1) [  x,    x,    0  ]
 *
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Maze
{
	private static List<MazeVertex> m_listMazeVerteces = null;
	private static List<WallPlacement> [] m_wallPlacements;
	private static int m_iWidth;
	private static int m_height;
	
	public static void Create (int p_iCol, int p_iRow)
	{
		m_iWidth = p_iCol;
		m_height = p_iRow;
		
        /* 2 x 2 Maze
         * cell -> (col, row) or (screen pos x, screen pos y)
         *
         *   | C O L
         * --|----------------------
         * R |       0       1
         * O | 1 [ (0,1) , (1,1) ]
         * W | 0 [ (0,0) , (1,0) ]
         *   |
         *
         */
		m_listMazeVerteces = new List<MazeVertex> (m_iWidth * m_height);
		for (int iRowIdx = 0; iRowIdx < m_height; ++iRowIdx)
		{
			for (int iColIdx = 0; iColIdx < m_iWidth; ++iColIdx)
			{
				m_listMazeVerteces.Add (new MazeVertex (iColIdx, iRowIdx));
			}
		}
		
		/* Adjacency matrix : used to determine neighbors and wall placements
         *                  : 'x' means not to be allocated
         *
         *           <------------------------
         *            (1,1)    (0,1)    (1,0)  | (0,0)
		 *  | (0,0) [  None,    None,    None  |   x  ] 
		 *  | (1,0) [  None,    None,     x    |   x  ]
		 *  v (0,1) [  None,     x,       x    |   x  ]
         *    ---------------------------------|-------
         *    (1,1) [   x,       x.       x,   |   x  ]
         *          
		 */
        int iLastMazeVertexIdx = m_listMazeVerteces.Count - 1;
        int iAdjMatrixSize = (m_height * m_iWidth) - 1;
		m_wallPlacements = new List<WallPlacement> [iAdjMatrixSize];
        
        for (int iRowIdx = 0; iRowIdx < iAdjMatrixSize; ++iRowIdx)
        {
            for (int iColIdx = 0; iColIdx < (iAdjMatrixSize - iRowIdx); ++iColIdx)
            {
                MazeVertex currentVertex  = m_listMazeVerteces [iRowIdx];
                MazeVertex neighborVertex = m_listMazeVerteces [iLastMazeVertexIdx - iColIdx];
                
//                Debug.Log ("====");
//                Debug.Log ("currentVertex: " + currentVertex.ToString() + " :: neighborVertex:" + neighborVertex.ToString ());
//                Debug.Log ("WallPlacement." + GetWallPlacement (currentVertex, neighborVertex));
                m_wallPlacements[iRowIdx].Add (GetWallPlacement (currentVertex, neighborVertex));
            }
        }
	}
	
	public static void Clear ()
	{
		m_iWidth = 0;
		m_height = 0;
		m_listMazeVerteces.Clear ();
	}
	
	public static bool IsEmpty ()
	{
		return m_listMazeVerteces == null && m_listMazeVerteces.Count == 0;
	}
    
    private static WallPlacement GetWallPlacement (MazeVertex p_currentVertex, MazeVertex p_possibleNeighbor)
    {
        /*
         * 4-way neighbor validation
         *
         *  + -> current cell
         *  o -> valid
         *  x -> invalid
         * 
         *  [ x, o, x ]
         *  [ o, +, o ]
         *  [ x, o, x ]
         *
         */
    
        int xOffset = p_possibleNeighbor.x - p_currentVertex.x;
        int yOffset = p_possibleNeighbor.y - p_currentVertex.y;
        Debug.Log ("xOffset: " + xOffset + ", yOffset: " + yOffset);
        
        if (Mathf.Abs (xOffset) > 1 || Mathf.Abs (yOffset) > 1)
        {
            return WallPlacement.None;
        }
        
        if (xOffset == 0)
        {
            if (yOffset > 0)
            {
                return WallPlacement.Up;
            }
            
            return WallPlacement.Down;
        }
        
        if (yOffset != 0)
        {
            return WallPlacement.None;
        }
        
        if (xOffset > 0)
        {
            return WallPlacement.Right;
        }
        
        return WallPlacement.Left;
    }
}

public struct MazeVertex
{
    public int x;
    public int y;
    
    public MazeVertex (int p_x, int p_y)
    {
        x = p_x;
        y = p_y;
    }
    
    public override string ToString ()
    {
        return "(" + x + ", " + y + ")";
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