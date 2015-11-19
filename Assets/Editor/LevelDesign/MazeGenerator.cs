/*
 * developer     : brian g. tria
 * creation date : 2015.11.20
 * description   : generates a random maze using dfs and adjacency matrix.
 * 
 */

using UnityEngine;
using UnityEditor;
using System.Collections;

public class MazeGenerator : EditorWindow
{
	private string m_strRowCount = "5";
	private string m_strColCount = "5";
	private int m_iRowCount;
	private int m_iColCount;

	protected void OnGUI ()
	{
		GUILayout.Label ("Width:");
		m_strColCount = GUILayout.TextField (m_strColCount);
		
		GUILayout.Label ("Height:");
		m_strRowCount = GUILayout.TextField (m_strRowCount);
	
		if (int.TryParse (m_strColCount, out m_iColCount) && int.TryParse (m_strRowCount, out m_iRowCount))
		{
			if (GUILayout.Button ("Generate Random Maze"))
			{
				Maze.Create (m_iColCount, m_iRowCount);
			}
		}
	}
	
	[MenuItem ("LevelDesign/Maze Generator")]
	public static void OpenGridGeneratorWindow ()
	{
		MazeGenerator gridGeneratorWindow = (MazeGenerator) EditorWindow.GetWindow (typeof (MazeGenerator));
		gridGeneratorWindow.titleContent = new GUIContent ("Maze Generator");
		gridGeneratorWindow.Show ();
	}
}

/*
data stored in json
- (half of) adjacency matrix
- start coordinate
- exit coordinate
- vertex spacing (for drawing maze)
- world position of vertex (0,0)
- level id
*/

/* Note:
 *   - generate a maze with size at least large enough to fill the whole screen plus allowance.
 *     this way, we can disregard thinking about borders / frame.
 *     after all, start and exit are not necessarily placed near the borders of the generated maze.
 */