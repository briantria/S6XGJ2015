/*
 * developer     : brian g. tria
 * creation date : 2015.10.27
 * 
 */

using UnityEngine;

public class Constants 
{
	public static readonly float PPU = 0.01f;
	public static readonly Vector2 ReferenceResolution = new Vector2 (2048, 1536);
	public static readonly string [] SortingLayerNames = new string []
	{
		"Default",
		"BGLayer",
		"GameLayer",
		"FGLayer",
		"UILayer",
		"LoadingLayer"
	};
	
	public static readonly string BGCamTag = "BGCamera";
}

public enum SortingLayerIDs
{
	Default      = 0,
	BGLayer      = 1,
	GameLayer    = 2,
	FGLayer      = 3,
	UILayer      = 4,
	LoadingLayer = 5
}
