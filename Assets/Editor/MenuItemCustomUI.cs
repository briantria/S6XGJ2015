/*
 * author : brian g. tria
 *   date : 2015.10.27
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class MenuItemCustomUI
{
	[MenuItem ("GameObject/CustomUI/Canvas", false, 10)]
	static void CreateCustomUI (MenuCommand p_menuCommand)
	{
		GameObject go = new GameObject ("Custom Canvas");
		go.layer = 5;
		
		GameObjectUtility.SetParentAndAlign(go, p_menuCommand.context as GameObject);
		Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
		Selection.activeObject = go;
		
		go.AddComponent <RectTransform>();
		Canvas canvas = go.AddComponent <Canvas>();
		CanvasScaler canvasScaler = go.AddComponent <CanvasScaler>();
		go.AddComponent <GraphicRaycaster>();
		
		canvas.renderMode = RenderMode.ScreenSpaceCamera;
		//canvas.sortingLayerName = Constants.SortingLayerNames [(int) SortingLayerIDs.UILayer];
		
		canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		canvasScaler.referenceResolution = new Vector2 (900, 600);
		canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
		canvasScaler.matchWidthOrHeight = 0.5f;
	}
}
