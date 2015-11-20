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
        if (MazeGeneratorData.State == MazeGeneratorState.ComputingPath)
        {
            GUILayout.Label ("Generating Maze...");
            return;
        }
        
        if (MazeGeneratorData.State == MazeGeneratorState.Saving)
        {
            GUILayout.Label ("Saving Maze...");
            return;
        }
        
        if (MazeGeneratorData.State == MazeGeneratorState.Fetching)
        {
            GUILayout.Label ("Loading Maze...");
            return;
        }
    
		GUILayout.Label ("Width:");
		m_strColCount = GUILayout.TextField (m_strColCount);
		
		GUILayout.Label ("Height:");
		m_strRowCount = GUILayout.TextField (m_strRowCount);
	
		if (int.TryParse (m_strColCount, out m_iColCount) && int.TryParse (m_strRowCount, out m_iRowCount))
		{
			if (GUILayout.Button ("Generate Random Maze"))
			{
                if (MazeGeneratorData.IsEmpty || MazeGeneratorData.IsSaved)
                {
                    MazeGeneratorData.Create (m_iColCount, m_iRowCount);
                }
                else
                {
                    int option = EditorUtility.DisplayDialogComplex
                                 (
                                    "Warning!",
                                    "Current changes will be lost. Save changes?",
                                    "Save Maze",
                                    "Proceed without Saving",
                                    "Cancel"
                                 );
                                 
                    switch (option)
                    {
                        case 0:
                        {
                            //Debug.Log ("save maze");
                            MazeGeneratorData.Save ();
                            break;
                        }
                        
                        case 1:
                        {
                            //Debug.Log ("proceed without saving");
                            MazeGeneratorData.Clear ();
                            MazeGeneratorData.Create (m_iColCount, m_iRowCount);
                            break;
                        }
                        
//                        case 2:
//                        {
//                            //Debug.Log ("cancel");
//                            break;
//                        }
                    }
                }
			}
		}
        
        if (MazeGeneratorData.IsEmpty == false)
        {
            if (GUILayout.Button ("Display Maze"))
            {
                GameObject objMaze = new GameObject ();
                Maze maze = objMaze.AddComponent <Maze>();
                maze.Display (MazeGeneratorData.MazeDimension);
            }
            
            if (GUILayout.Button ("Clear Maze"))
            {
                if (MazeGeneratorData.IsSaved == false &&
                    EditorUtility.DisplayDialog (
                        "Clear Maze",
                        "Save changes?",
                        "Save",
                        "Proceed without Saving"))
                {
                    MazeGeneratorData.Save ();
                }
                
                MazeGeneratorData.Clear ();
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
- neighbor (adjacency) matrix
- maze dimension
- start coordinate
- exit coordinate
- level id
*/

/* Note:
 *   - generate a maze with size at least large enough to fill the whole screen plus allowance.
 *     this way, we can disregard thinking about borders / frame.
 *     after all, start and exit are not necessarily placed near the borders of the generated maze.
 */