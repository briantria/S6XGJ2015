/*
 * developer     : brian g. tria
 * creation date : 2015.12.17
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultScreenManager : MonoBehaviour 
{
	[SerializeField] private Text m_textResult;

	private static ResultScreenManager m_instance = null;
	public  static ResultScreenManager Instance {get {return m_instance;}}
	
	public ResultEnum GameResult { set; get; }
	
	private Canvas m_canvas;
	
	protected void Awake ()
	{
		m_instance = this;
		m_canvas = this.GetComponent<Canvas> ();
		m_canvas.enabled = false;
	}
	
	public void Open ()
	{
		m_canvas.enabled = true;
	}
	
	public void Close ()
	{
		m_canvas.enabled = false;
	}
	
	public void SetResults (ResultEnum p_resultEnum)
	{
		GameResult = p_resultEnum;
	
		switch (p_resultEnum) {
		case ResultEnum.Win:
		{
			m_textResult.text = "Yun oh!";
			break;
		}
		case ResultEnum.Lose:
		{
			m_textResult.text = "Weak!";
			break;
		}}
	}
}

[System.Flags]
public enum ResultEnum
{
	None,
	Win,
	Lose
}
