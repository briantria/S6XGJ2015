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
    [SerializeField] private List<GameObject> m_listWalls = new List<GameObject> ();
    private Dictionary<WallPlacement, GameObject> m_dictWalls;
    
    public int Id { get; set; }
    public IntVector2 Coordinates { get; set; }
    
    protected void Awake ()
    {
        m_dictWalls = new Dictionary<WallPlacement, GameObject> ()
        {
            { WallPlacement.Up, m_listWalls [0] },
            { WallPlacement.Right, m_listWalls[1] }
        };
    }
}