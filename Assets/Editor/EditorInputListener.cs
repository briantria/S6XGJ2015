/*
 * developer     : brian g. tria
 * creation date : 2015.11.09
 * 
 */

using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(Tile))]
public class EditorInputListener : Editor 
{
	void OnSceneGUI ()
	{
		Event e = Event.current;
		Tile tile = (Tile) target;
		
		switch (e.type)
		{
			case EventType.mouseUp:
			{
				Debug.Log ("MOUSE UP!");
				tile.Test ();
				Event.current.Use ();
				break;
			}
		}
	}
}
