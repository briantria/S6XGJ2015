/*
 * developer     : brian g. tria
 * creation date : 2015.11.03
 * 
 */

using UnityEngine;
using System.Collections;

public class Xyxy : MonoBehaviour 
{
	[SerializeField] private SpriteRenderer m_spriteRenderer;
	
	private XyxyData m_xyxyData;
	
	public void Create (XyxyType p_type)
	{
		m_xyxyData = new XyxyData (p_type);
	}
	
	public void Edit (XyxyType p_type)
	{
		m_xyxyData.Edit (p_type);
	}
}
