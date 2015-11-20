/*
 * developer     : brian g. tria
 * creation date : 2015.11.20
 *
 */

using UnityEngine;
using System.Collections;

public class Maze : MonoBehaviour
{
	public void Display (IntVector2 p_iv2Dimension)
    {
        // TODO: Object pooling
        
        for (int iRow = 0; iRow < p_iv2Dimension.y; ++iRow)
        {
            for (int iCol = 0; iCol < p_iv2Dimension.x; ++iCol)
            {
                
            }
        }
    }
}

[System.Flags]
public enum WallPlacement
{
    None,
    Left,
    Up,
    Right,
    Down
}