/*
 * developer     : brian g. tria
 * creation date : 2015.12.01
 *
 */

using UnityEngine;
using System.Collections;

public class HexButtonManager : MonoBehaviour
{
    [SerializeField] private Collider2D     m_collider;
    [SerializeField] private SpriteRenderer m_spriteRendererBody;
    [SerializeField] private SpriteRenderer m_spriteRendererFace;
	
    private bool m_bIsEmpty = true;
    private Transform m_transform;
    private Vector2 m_spriteSize;
    private PlayerType m_playerType;
    
    public PlayerType PlayerType {get {return m_playerType;}}
    
    protected void OnEnable ()
    {
        GameManager.OnGamePhaseUpdate += OnGamePhaseUpdate;
    }
    
    protected void OnDisable ()
    {
        GameManager.OnGamePhaseUpdate -= OnGamePhaseUpdate;
    }
    
    protected void Awake ()
    {
        m_transform = this.transform;
        m_spriteSize = m_spriteRendererBody.sprite.bounds.size;
        m_playerType = PlayerType.None;
    }
    
    protected void OnTriggerEnter2D (Collider2D p_collider)
    {
		if (GameManager.Instance.CurrentGamePhase == GamePhase.Play && !m_bIsEmpty
	    &&  p_collider.CompareTag ("Player"))
    	{
    		AnimateOut ();
    		PlayerController.Instance.AddPlayerHex (m_playerType);
    	}
    }
    
    private void OnGamePhaseUpdate (GamePhase p_gamePhase)
    {
        //m_HexButtonManager.gameObject.SetActive (p_gamePhase == GamePhase.Edit);
        m_collider.enabled = ((p_gamePhase == GamePhase.Edit) || !m_bIsEmpty);
        m_spriteRendererBody.enabled = ((p_gamePhase == GamePhase.Edit) || !m_bIsEmpty);
        m_spriteRendererFace.enabled = ((p_gamePhase == GamePhase.Edit) || !m_bIsEmpty);
    }
    
    private void AsyncAnimateOut ()
    {
        m_spriteRendererFace.enabled = false;
        m_spriteRendererBody.enabled = false;
        m_collider.enabled = false;
    }
    
    public void AnimateOut ()
    {
        Invoke ("AsyncAnimateOut", 0.1f);
    }
    
    public void OnExtendWalls (float p_scale)
    {
        Vector3 position = m_transform.position;
        position.x -= (m_spriteSize.x * p_scale * 0.05f);
        position.y -= (m_spriteSize.y * p_scale * 0.1f);
        m_transform.position = position;
    }
    
    public void OnHexSetupPanelResult (PlayerType p_playerType)
    {
        m_playerType = p_playerType;
        m_bIsEmpty = ((p_playerType | PlayerType.None) == 0);
        m_spriteRendererBody.sprite = PlayerTypeInfo.Instance.SpriteBody;
        
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