/*
 * developer     : brian g. tria
 * creation date : 2015.11.03
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum XyxyType
{
	Drowxy = 1 << 0,
	Xhy    = 1 << 1,
	Quirxy = 1 << 2,
	Flexy  = 1 << 3,
	Geexy  = 1 << 4,
	Xauxy  = 1 << 5
}

public class XyxyData
{
	readonly private Dictionary <XyxyType, Color> m_dictColors = new Dictionary <XyxyType, Color>
	{
		{XyxyType.Drowxy, new Color (144, 189, 211, 255)},
		{XyxyType.Xhy,    new Color (225, 106,  92, 255)},
		{XyxyType.Quirxy, new Color (103, 208, 206, 255)},
		{XyxyType.Flexy,  new Color (244, 242, 144, 255)},
		{XyxyType.Geexy,  new Color (203, 109, 233, 255)},
		{XyxyType.Xauxy,  new Color (189, 221,  99, 255)}
	};

	private XyxyType m_type;
	private Color    m_color;
	
	public XyxyType Type {get { return m_type; }}
	public Color    RGBA {get { return m_color; }}
	
	public XyxyData (XyxyType p_type)
	{
		Edit (p_type);
	}
	
	public void Edit (XyxyType p_type)
	{
		m_type = p_type;
		m_color = m_dictColors [p_type];
	}
}
