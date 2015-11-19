/*
 * developer     : brian g. tria
 * creation date : 2015.11.04
 * 
 */

using UnityEngine;
using UnityEditor;
using System.Collections;

public class DesignGrid : EditorWindow 
{
	private Vector2 m_v2GridSize = Vector2.one;
	string m_strGridX = "15";
	string m_strGridY = "10";

	void OnGUI ()
	{
		GUILayout.Label ("Column Count:");
		m_strGridX = GUILayout.TextField (m_strGridX);
		
		GUILayout.Label ("Row Count:");
		m_strGridY = GUILayout.TextField (m_strGridY);
		
		int iGridColCount, iGridRowCount;
		if (int.TryParse (m_strGridX, out iGridColCount) && int.TryParse (m_strGridY, out iGridRowCount))
		{
			m_v2GridSize.x = iGridColCount;
			m_v2GridSize.y = iGridRowCount;
			
			if (GUILayout.Button ("Create Grid"))
			{
				//Debug.Log ("Create Grid: " + Constants.SortingLayerNames [0]);
				GameObject gridContainer = GameObject.Find ("GridContainer");
				if (gridContainer == null)
				{
					gridContainer = new GameObject ("GridContainer");
				}
				
				// TODO:
				//   Consider creating a matrix with 0s and 1s first
				//   Then instantiate the tiles based on the matrix
				
				for (int iRowIdx = 0; iRowIdx < iGridColCount; ++iRowIdx)
				{
					for (int iColIdx = 0; iColIdx < iGridRowCount; ++iColIdx)
					{
						GameObject gObj = Instantiate <GameObject>(Resources.Load <GameObject>(Tile.PREFAB_PATH));
						Tile td = gObj.GetComponent <Tile> ();
						td.AddTo (gridContainer.transform, new Vector2 (iRowIdx, iColIdx));
						td.SetType (Random.Range (0, 2) > 0 ? TileType.Wall : TileType.Path);
					}
				}
			}
		}
	}
}
