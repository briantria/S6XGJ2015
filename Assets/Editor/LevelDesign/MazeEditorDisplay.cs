/*
 * developer     : brian g. tria
 * creation date : 2015.11.21
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeEditorDisplay : Maze
{
    public void LoadWallPlacements (List<WallPlacement> [] p_wallPlacements)
    {
        m_wallPlacements = p_wallPlacements;
    }
}