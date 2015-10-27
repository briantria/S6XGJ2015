/*
 * author : brian g. tria
 *   date : 2015.10.27
 * 
 */

using UnityEngine;

public class Constants 
{
	public static readonly string [] SortingLayerNames = new string []
	{
		"Default",
		"BGLayer",
		"GameLayer",
		"FGLayer",
		"UILayer",
		"LoadingLayer"
	};
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
