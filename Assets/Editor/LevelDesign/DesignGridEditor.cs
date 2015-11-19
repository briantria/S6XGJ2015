/*
 * developer     : brian g. tria
 * creation date : 2015.11.04
 *
 */

using UnityEngine;
using UnityEditor;
using System.Collections;

public class DesignGridEditor : EditorWindow
{
	private string m_jsonFilePath = string.Empty;

	[MenuItem ("LevelDesign/Grid Generator")]
	static void OpenGridGeneratorWindow ()
	{
		DesignGridEditor gridGeneratorWindow = (DesignGridEditor) EditorWindow.GetWindow (typeof (DesignGridEditor));
		gridGeneratorWindow.titleContent = new GUIContent ("Grid Generator");
		gridGeneratorWindow.Show ();
	}
	
	void OnGUI ()
	{
		if (GUILayout.Button ("Generate New Grid"))
		{
			m_jsonFilePath = string.Empty;
			
			DesignGrid designGridWindow = ScriptableObject.CreateInstance <DesignGrid>();
			designGridWindow.ShowUtility ();
		}
		
		if (GUILayout.Button ("Open"))
		{
			Debug.Log ("Select and load map data from json: ");
		}
		
		if (GUILayout.Button ("Save"))
		{
			if (string.IsNullOrEmpty (m_jsonFilePath))
			{
				// goto default save folder
				// create new file
			}
			else
			{
				// overwrite existing json
			}
			
			Debug.Log ("Save: Overwrite json file");
		}
	}
}