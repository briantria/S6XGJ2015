/*
 * developer     : brian g. tria
 * creation date : 2015.11.04
 * 
 */

using UnityEngine;
using System.Collections;

public class TileData : ScriptableObject
{
	#region Constants
	public static readonly Vector2 Size = new Vector2 (124.0f, 108.0f);
	private const string  PREFAB_PATH = "Prefabs/Tile";
	#endregion
	
	#region Properties
	public Vector2 Index {get {return m_v2Index;}}
	#endregion
	
	private Vector2 m_v2Index = Vector2.zero;
		
	public void AddTo (Transform p_tParent, Vector2 p_v2Index)
	{
		GameObject gObj = Instantiate <GameObject>(Resources.Load <GameObject>(PREFAB_PATH));
				   gObj.name = p_v2Index.x + ", " + p_v2Index.y;
		
		float scale = Camera.main.aspect / (Constants.ReferenceResolution.x / Constants.ReferenceResolution.y);
		
		Transform trans = gObj.transform;
				  trans.SetParent (p_tParent);
				  trans.localScale = Vector3.one * scale;
		
		m_v2Index = p_v2Index;
		
		Vector3 pos    = trans.position;
				pos.x *= m_v2Index.x * Size.x;
				//pos.x -= m_v2Index.x * Size.x * 0.25f;
				pos.y *= m_v2Index.y * Size.y;
				
		if (m_v2Index.y % 2 == 1)
		{
			pos.y += Size.x * 0.5f;
		}
		
		pos.x *= Constants.PPU;
		pos.y *= Constants.PPU;
		
		trans.position = pos;
	}
}
