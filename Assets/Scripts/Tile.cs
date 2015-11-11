/*
 * developer     : brian g. tria
 * creation date : 2015.11.04
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum TileType
{
	Path,
	Wall,
	Mount,
	SpawnPoint,
	Start,
	Exit
}

public class Tile : MonoBehaviour
{
	#region Constants
	public static readonly Vector2 Size = new Vector2 (128, 128); //new Vector2 (124.0f, 108.0f);
	public static readonly string  PREFAB_PATH = "Prefabs/Tile";
	
	private Dictionary <TileType, Color> m_dictTileColor = new Dictionary <TileType, Color> ()
	{
		{TileType.Path, Color.white},
		{TileType.Wall, Color.black}
	};
	#endregion
	
	#region Properties
	public Vector2 Index {get {return m_v2Index;}}
	public TileType Type {get {return m_tileType;}}
	#endregion
	
	[SerializeField] private SpriteRenderer m_spriteRenderer;
	
	private Transform m_transform;
	private GameObject m_gameObject;
	private Vector2 m_v2Index = Vector2.zero;
	private TileType m_tileType;
	
	protected void Awake ()
	{
		m_transform = this.transform;
		m_gameObject = this.gameObject;
		m_tileType = TileType.Path;
	}
	
	protected void OnMouseUpAsButton ()
	{
//		GameObject gObjRaycastClick = InputManager.RaycastClick ();
//	
//		if (gObjRaycastClick != null && m_gameObject.GetInstanceID () != m_gameObject.GetInstanceID ())
//		{
//			Debug.Log ("RAYCAST! BITCHES!!!!");
//			return;
//		}
		
		if (InputManager.OverlapClick () > 1)
		{
			return;
		}
		
		switch (m_tileType){
		case TileType.Path:
		{
			XyxySetupPanel.Instance.Open ();
			XyxySetupPanel.Instance.SetPosition (m_transform.position);
			break;
		}}
	}
	
//	protected void Update ()
//	{
//		if (Input.GetMouseButtonUp (0))
//		{
//			if (InputManager.ClickToTargetDistance (m_transform.position) < Size.x * Constants.PPU * 0.3f)
//			{
//				switch (m_tileType){
//				case TileType.Path:
//				{
//					XyxySetupPanel.Instance.Open ();
//					XyxySetupPanel.Instance.SetPosition (m_transform.position);
//					break;
//				}}
//			}
//		}
//	}
								
	public void AddTo (Transform p_tParent, Vector2 p_v2Index)
	{
		GameObject gObj = this.gameObject;
				   gObj.name = p_v2Index.x + ", " + p_v2Index.y;
		
		float scale = Camera.main.aspect / (Constants.ReferenceResolution.x / Constants.ReferenceResolution.y);
		
		Transform trans = gObj.transform;
				  trans.SetParent (p_tParent);
				  trans.localScale = Vector3.one * scale;
				  trans.position = Vector3.zero;
		
		m_v2Index = p_v2Index;
		
		Vector3 pos    = trans.position;
				pos.x  = m_v2Index.x * Size.x;
				pos.y  = m_v2Index.y * Size.y;
			
		pos.x *= Constants.PPU;
		pos.y *= Constants.PPU;
		
		trans.position = pos;
	}
	
	public void SetType (TileType p_tileType)
	{
		m_tileType = p_tileType;
		m_spriteRenderer.color = m_dictTileColor [m_tileType];
	}
	
	#if UNITY_EDITOR
	public void ToggleTileType ()
	{
		switch (m_tileType){
		case TileType.Path:
		{
			SetType (TileType.Wall);
			break;
		}
		case TileType.Wall:
		{
			SetType (TileType.Path);
			break;
		}}
	}
	#endif
}
