/*
 * author : brian g. tria
 *   date : 2015.10.27
 * 
 */

public class HudName 
{
	private HudId  m_hudId;
	private string m_strHudName;
	
	public HudName (HudId p_hudId, string p_strHudName)
	{
		this.m_hudId = p_hudId;
		this.m_strHudName = p_strHudName;
	}
	
	public static explicit operator int (HudName p_hudNameInstance)
	{
		return (int) p_hudNameInstance.m_hudId;
	}
	
	public static explicit operator string (HudName p_hudNameInstance)
	{
		return p_hudNameInstance.m_strHudName;
	}
	
	public static explicit operator HudId (HudName p_hudNameInstance)
	{
		return p_hudNameInstance.m_hudId;
	}
	
//	public static explicit operator HudName (int p_iId)
//	{
//		
//		return this;
//	}
//	
//	public static explicit operator string (HudName p_hudNameInstance)
//	{
//		return p_hudNameInstance.m_strHudName;
//	}
//	
//	public static explicit operator HudId (HudName p_hudNameInstance)
//	{
//		return p_hudNameInstance.m_hudId;
//	}
}

public enum HudId 
{
	Home,
	Game,
	LoadingScreen,
	Settings
}