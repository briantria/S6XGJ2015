/*
 * developer     : brian g. tria
 * creation date : 2015.12.01
 *
 */

using UnityEngine;
using System.Collections;

public class HexButtonManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteRenderer;
	private bool m_bIsEmpty = true;
    
    protected void OnEnable ()
    {
        GameManager.OnGamePhaseUpdate += OnGamePhaseUpdate;
    }
    
    protected void OnDisable ()
    {
        GameManager.OnGamePhaseUpdate -= OnGamePhaseUpdate;
    }
    
    private void OnGamePhaseUpdate (GamePhase p_gamePhase)
    {
        //m_HexButtonManager.gameObject.SetActive (p_gamePhase == GamePhase.Edit);
        m_spriteRenderer.enabled = ((p_gamePhase == GamePhase.Edit) || !m_bIsEmpty);
    }
    
    public void OnHexSetupPanelResult (PlayerType p_playerType)
    {
        m_bIsEmpty = ((p_playerType | PlayerType.None) == 0);
        
//        if (m_bIsEmpty)
//        {
//            m_spriteRenderer.sprite = MazeImageLoader.Instance.SpriteHexButton;
//        }
//        else
//        {
            m_spriteRenderer.sprite = PlayerTypeInfo.Instance.SpriteBody;
//        }
        
        m_spriteRenderer.color = PlayerTypeInfo.Instance.PlayerColor [p_playerType];
        // replace with face dictionary
//        Debug.Log (PlayerTypeInfo.Instance.PlayerColor [p_playerType]);
    }
}