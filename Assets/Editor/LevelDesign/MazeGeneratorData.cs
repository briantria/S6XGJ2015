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

// TODO: fully functional scriptable object
public class MazeGeneratorData : ScriptableObject
{
    #region Constants
    private static readonly IntVector2 [] k_iv2NeighborOffsets = new IntVector2[]
    {
        new IntVector2 (-1,  0), // left
        new IntVector2 ( 0,  1), // up
        new IntVector2 ( 1,  0), // right
        new IntVector2 ( 0, -1)  // down
    };
    #endregion

    private static List<IntVector2> m_listMazeVerteces = null;
	private static List<WallPlacement> [] m_wallPlacements;
    private static int [,] m_visitMatrix;
    
	private static int m_iWidth;
	private static int m_height;
    private static bool m_saved;
    private static MazeGeneratorState m_generatorSate = MazeGeneratorState.Ready;
    private static MazeEditorDisplay m_mazeEditorDisplay = null;
    
    #region Properties
    public static IntVector2 MazeDimension {get {return new IntVector2 (m_iWidth, m_height);}}
    public static MazeGeneratorState State {get {return m_generatorSate;}}
    public static List<WallPlacement> [] WallPlacementData {get {return m_wallPlacements;}}
    public static bool IsSaved {get {return m_saved;}}
    public static bool IsEmpty 
    {
        get {return m_listMazeVerteces == null || m_listMazeVerteces.Count == 0;}
    }
    #endregion
	
	public static void Create (int p_iCol, int p_iRow)
	{
		m_iWidth = p_iCol;
		m_height = p_iRow;
        m_saved = false;
		
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
         
        m_listMazeVerteces = new List<IntVector2> (m_iWidth * m_height);
        m_visitMatrix = new int[m_iWidth, m_height];
		
        for (int iRowIdx = 0; iRowIdx < m_height; ++iRowIdx)
		{
			for (int iColIdx = 0; iColIdx < m_iWidth; ++iColIdx)
			{
                m_visitMatrix [iColIdx, iRowIdx] = 0;
				m_listMazeVerteces.Add (new IntVector2 (iColIdx, iRowIdx));
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
         
        int iSignificantVertexCount = m_listMazeVerteces.Count - 1;
        m_wallPlacements = new List<WallPlacement> [iSignificantVertexCount];
        
        for (int iRowIdx = 0; iRowIdx < iSignificantVertexCount; ++iRowIdx)
        {
            m_wallPlacements[iRowIdx] = new List<WallPlacement> ();
        
            for (int iColIdx = 0; iColIdx < (iSignificantVertexCount - iRowIdx); ++iColIdx)
            {
                IntVector2 currentVertex  = m_listMazeVerteces [iRowIdx];
                IntVector2 neighborVertex = m_listMazeVerteces [iSignificantVertexCount - iColIdx];
                m_wallPlacements[iRowIdx].Add (GetWallPlacement (currentVertex, neighborVertex));
            }
        }
        
        GenerateRandomPath ();
        //DebugPrintWallData ();
        
        if (m_mazeEditorDisplay == null)
        {
            m_mazeEditorDisplay = GameObject.FindObjectOfType<MazeEditorDisplay> ();
            if (m_mazeEditorDisplay == null)
            {
                GameObject objMazeEditor = new GameObject ("MazeEditorDisplay");
                m_mazeEditorDisplay = objMazeEditor.AddComponent<MazeEditorDisplay> ();
            }
        }
        
        m_mazeEditorDisplay.LoadWallPlacements (m_wallPlacements);
        m_mazeEditorDisplay.Display (MazeDimension);
	}
	
	public static void Clear ()
	{
		m_iWidth = 0;
		m_height = 0;
		m_listMazeVerteces.Clear ();
        m_wallPlacements = null;
        m_visitMatrix = null;
        DestroyImmediate (m_mazeEditorDisplay.gameObject);
        
        System.GC.Collect ();
	}
    
    public static void Save ()
    {
        LevelData levelData = new LevelData ();
        levelData.MazeHeight = m_height;
        levelData.MazeWidth = m_iWidth;
        levelData.StartPointID = 0;
        levelData.EndPointID = 0;
        levelData.WallPlacementFlags = m_wallPlacements;
        
        LevelDataManager.Instance.Save (levelData);
    }
    
    public static void Load ()
    {
        LevelDataManager.Instance.Load ();
    }
    
    private static void GenerateRandomPath ()
    {
        /**************************************************************************
        
            GENERATE RANDOM MAZE PATH
                1. get random initial m_listMazeVerteces index (push to backtrack stack and mark visitMatrix [x][y] = 1)
                2. GET RANDOM NEIGHBOR
                   if all neighbors are visited, pop from backtrack. if backtrack is empty, end; else (backtrack not empty) repeat 2.
                3. if neighbor is valid and unvisited (visitMatrix [x][y] == 0); mark it visited, push to backtrack and UPDATE WALL PLACEMENTS MATRIX.
                   else (invalid or visited neigbor) go back to 2.
                
            GET RANDOM NEIGHBOR
                1. n = 4;
                2. index = random.range (0,n)
                3. if neighbor[index] != valid, n--
                4. if n <= 0, no more valid neighbor. else, repeat 2
            
            UPDATE WALL PLACEMENTS MATRIX
                1. get m_wallPlacements rowIdx using backtrack [current-1]
                2. get m_wallPlacements colIdx using backtrack [current]
                3. mark m_wallPlacements [rowIdx][colIdx] = none
                
        *************************************************************************/
        
        List<IntVector2> iv2BactrackStack = new List<IntVector2> ();
        IntVector2 iv2Neighbor;
        int iBactrackCount;
        
        // push random root
        iv2BactrackStack.Add (m_listMazeVerteces [Random.Range (0, m_listMazeVerteces.Count)]);
        m_visitMatrix [iv2BactrackStack[0].x, iv2BactrackStack[0].y] = 1;
        iBactrackCount = iv2BactrackStack.Count;
        
        while (iBactrackCount > 0)
        {
            if (GetRandomUnvisitedNeighbor (iv2BactrackStack [iBactrackCount - 1], out iv2Neighbor))
            {
                RemoveWallBetween (iv2BactrackStack [iBactrackCount - 1], iv2Neighbor);
                // push neighbor vertex
                iv2BactrackStack.Add (iv2Neighbor);
            }
            else // all neighbors are already visited
            {
                // pop current vertex
                iv2BactrackStack.RemoveAt (iBactrackCount - 1);
            }
            
            iBactrackCount = iv2BactrackStack.Count;
        }
    }
    
    private static bool GetRandomUnvisitedNeighbor (IntVector2 p_iv2Current, out IntVector2 p_iv2Neighbor)
    {
        bool bValidNeighbor = false;
        List<int> m_listIndeces = new List<int> () {0, 1, 2, 3};
        int index;
        
        do
        {
            index = Random.Range (0, m_listIndeces.Count);
            p_iv2Neighbor = p_iv2Current.Sum (k_iv2NeighborOffsets [m_listIndeces[index]]);
            
            // choose another neigbor if this is outside the maze
            if (   p_iv2Neighbor.x < 0
                || p_iv2Neighbor.y < 0
                || p_iv2Neighbor.x >= m_iWidth
                || p_iv2Neighbor.y >= m_height )
            {
                m_listIndeces.RemoveAt (index);
                continue;
            }
            
            // choose another neigbor if this was already visited
            if (m_visitMatrix [p_iv2Neighbor.x, p_iv2Neighbor.y] == 1)
            {
                m_listIndeces.RemoveAt (index);
                continue;
            }
            
            // valid neighbor found. mark this as visited
            bValidNeighbor = true;
            m_visitMatrix [p_iv2Neighbor.x, p_iv2Neighbor.y] = 1;
            break;
        }
        while (m_listIndeces.Count > 0);
        
        return bValidNeighbor;
    }
    
    private static void RemoveWallBetween (IntVector2 p_iv2Current, IntVector2 p_iv2Neighbor)
    {
        /* Adjacency matrix : used to determine neighbors and wall placements
         *                  : 'x' means not to be allocated
         *
         *                         N E I G H B O R 
         *                     0        1        2        3
         *                  <------------------------
         *  C                (1,1)    (0,1)    (1,0)  | (0,0)
         *  U   0  | (0,0) [  None,    None,    None  |   x  ] 
         *  R   1  | (1,0) [  None,    None,     x    |   x  ]
         *  R   2  v (0,1) [  None,     x,       x    |   x  ]
         *  E        ---------------------------------|-------
         *  N   3    (1,1) [   x,       x.       x,   |   x  ]
         *  T
         *       
         */
        
        int iRowIdx;
        int iColIdx;
        
        iRowIdx = (m_iWidth * p_iv2Current.y) + p_iv2Current.x;
        iColIdx = (m_height * m_iWidth) - 1;
        
        // if either iRowIdx or iColIdx is out of bounds, switch them
        if (iRowIdx < 0 || iRowIdx >= m_wallPlacements.Length)
        {
            iRowIdx  = (m_iWidth * p_iv2Neighbor.y) + p_iv2Neighbor.x;
            iColIdx -= (m_iWidth * p_iv2Current.y) + p_iv2Current.x;
        }
        else
        {
            iColIdx -= (m_iWidth * p_iv2Neighbor.y) + p_iv2Neighbor.x;
            
            if (iColIdx < 0 || iColIdx >= m_wallPlacements[iRowIdx].Count)
            {
                // reset iColIdx then switch
                iRowIdx  = (m_iWidth * p_iv2Neighbor.y) + p_iv2Neighbor.x;
                iColIdx  = (m_height * m_iWidth) - 1;
                iColIdx -= (m_iWidth * p_iv2Current.y) + p_iv2Current.x;
            }
        }
        
        m_wallPlacements [iRowIdx] [iColIdx] = WallPlacement.None;
    }
    
    private static WallPlacement GetWallPlacement (IntVector2 p_currentVertex, IntVector2 p_possibleNeighbor)
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
    
    #region DEBUG
    private static void DebugPrintWallData ()
    {
        string str = "\t";
        
        for (int idx = m_listMazeVerteces.Count-1; idx > 0; --idx)
        {
            str += m_listMazeVerteces[idx].ToString () + "\t";
        }
        
        for (int idx = 0; idx < m_wallPlacements.Length; ++idx)
        {
            str += "\n" + m_listMazeVerteces[idx].ToString ();
            for (int jdx = 0; jdx < m_wallPlacements[idx].Count; ++jdx)
            {
                str += "\t" + m_wallPlacements[idx][jdx];
            }
        }
        
        Debug.Log (str + "\n");
    }
    #endregion
}

[System.Flags]
public enum MazeGeneratorState
{
    Ready,
    ComputingPath,
    Saving,
    Fetching,
    Done
}