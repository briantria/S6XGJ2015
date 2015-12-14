/*
 * developer     : brian g. tria
 * creation date : 2015.12.01
 *
 */

using UnityEngine;
using System.Collections;

public class HexButtonManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteRendererBody;
    [SerializeField] private SpriteRenderer m_spriteRendererFace;
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
        m_spriteRendererBody.enabled = ((p_gamePhase == GamePhase.Edit) || !m_bIsEmpty);
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
            m_spriteRendererBody.sprite = PlayerTypeInfo.Instance.SpriteBody;
//        }
        
        if (p_playerType == PlayerType.None)
        {
            m_spriteRendererBody.color = PlayerTypeInfo.Instance.PlayerColor [p_playerType];
            m_spriteRendererFace.sprite = null;
        }
        else
        {
            m_spriteRendererBody.color = Color.white;
            m_spriteRendererFace.sprite = PlayerTypeInfo.Instance.PlayerFace [p_playerType];
        }
    }
}