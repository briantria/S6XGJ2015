/*
 * developer     : brian g. tria
 * creation date : 2015.11.20
 * description   : generates a random maze using dfs and adjacency matrix.
 * 
 */

using UnityEngine;
using UnityEditor;
using System.Collections;

public class MazeEditor : EditorWindow
{
	private string m_strRowCount = "5";
	private string m_strColCount = "5";
	private int m_iRowCount;
	private int m_iColCount;

	protected void OnGUI ()
	{
        if (MazeGenerator.State == MazeGeneratorState.ComputingPath)
        {
            GUILayout.Label ("Generating Maze...");
            return;
        }
        
        if (MazeGenerator.State == MazeGeneratorState.Saving)
        {
            GUILayout.Label ("Saving Maze...");
            return;
        }
        
        if (MazeGenerator.State == MazeGeneratorState.Fetching)
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
                if (MazeGenerator.IsEmpty || MazeGenerator.IsSaved)
                {
                    MazeGenerator.Create (m_iColCount, m_iRowCount);
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
                            MazeGenerator.Save ();
                            break;
                        }
                        
                        case 1:
                        {
                            //Debug.Log ("proceed without saving");
                            MazeGenerator.Clear ();
                            MazeGenerator.Create (m_iColCount, m_iRowCount);
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
        
        if (GUILayout.Button ("Load Maze"))
        {
            if (MazeGenerator.Load () == false)
            {
                EditorUtility.DisplayDialog ("Load Maze", "Load failed. No level data saved.", "Ok");
            }
        }
        
        if (MazeGenerator.IsEmpty == false)
        {
            if (GUILayout.Button ("Save Maze"))
            {
                MazeGenerator.Save ();
            }
        
            if (GUILayout.Button ("Clear Maze"))
            {
                if (MazeGenerator.IsSaved == false &&
                    EditorUtility.DisplayDialog (
                        "Clear Maze",
                        "Save changes?",
                        "Save",
                        "Proceed without Saving"))
                {
                    MazeGenerator.Save ();
                }
                
                MazeGenerator.Clear ();
            }
        }
	}
	
	[MenuItem ("LevelDesign/Maze Generator")]
	public static void OpenGridGeneratorWindow ()
	{
		MazeEditor gridGeneratorWindow = (MazeEditor) EditorWindow.GetWindow (typeof (MazeEditor));
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