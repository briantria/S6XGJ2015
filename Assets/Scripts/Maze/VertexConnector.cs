/*
 * developer     : brian g. tria
 * creation date : 2015.11.30
 *
 */

using UnityEngine;
using System.Collections;

public class VertexConnector : MonoBehaviour
{
    #region Constants
    /*
     *  Connector cases: --, |, L, <, 7, J, T, --|, _|_, |--, +
     *  Case bit representation : 00 00 00
     *      - from leftmost bit : Top neighbor's right wall flag
     *                            Right neighbor's up wall flag
     *                            This wall flag
     *
     *                    Neighbor Up   Neighbor Right     This
     *  Quick reference :   right up  |    right up    | right up
     */
    private const int CONNECTOR_H  = 5; //0x000101; // --
    private const int CONNECTOR_V  = 34;//0x100010; //  |
    private const int CONNECTOR_L0 = 36;//0x100100; //  L
    private const int CONNECTOR_L1 = 6; //0x000110; //  <
    private const int CONNECTOR_L2 = 3; //0x000011; //  7
    private const int CONNECTOR_L3 = 33;//0x100001; //  J
    private const int CONNECTOR_T0 = 7; //0x000111; //  T
    private const int CONNECTOR_T1 = 35;//0x100011; // --|
    private const int CONNECTOR_T2 = 37;//0x100101; // _|_
    private const int CONNECTOR_T3 = 38;//0x100110; // |--
    private const int CONNECTOR_X  = 39;//0x100111; //  +
    #endregion
    
    private SpriteRenderer m_spriteRenderer;
    
    protected void Awake ()
    {
        m_spriteRenderer = this.GetComponent<SpriteRenderer> ();
    }
    
    public void Enable (bool p_bEnable)
    {
        m_spriteRenderer.enabled = p_bEnable;
    }
    
    public void Setup (int p_iConnectorType)
    {
        int zRotation = 0;
    
        switch (p_iConnectorType)
        {
        case CONNECTOR_H: // --
        {
            m_spriteRenderer.sprite = MazeImageLoader.Instance.PipeI;
            zRotation = 0;
            break;
        }
            
        case CONNECTOR_V: // |
        {
            m_spriteRenderer.sprite = MazeImageLoader.Instance.PipeI;
            zRotation = 90;
            break;
        }
            
        case CONNECTOR_L0: // L
        {
            m_spriteRenderer.sprite = MazeImageLoader.Instance.PipeL;
            zRotation = 90;
            break;
        }
            
        case CONNECTOR_L1: // <
        {
            m_spriteRenderer.sprite = MazeImageLoader.Instance.PipeL;
            zRotation = 0;
            break;
        }
            
        case CONNECTOR_L2: // 7
        {
            m_spriteRenderer.sprite = MazeImageLoader.Instance.PipeL;
            zRotation = 270;
            break;
        }
            
        case CONNECTOR_L3: // J
        {
            m_spriteRenderer.sprite = MazeImageLoader.Instance.PipeL;
            zRotation = 180;
            break;
        }
            
        case CONNECTOR_T1: // --|
        {
            m_spriteRenderer.sprite = MazeImageLoader.Instance.PipeT;
            zRotation = 90;
            break;
        }
            
        case CONNECTOR_T2: // _|_
        {
            m_spriteRenderer.sprite = MazeImageLoader.Instance.PipeT;
            zRotation = 0;
            break;
        }
            
        case CONNECTOR_T3: // |--
        {
            m_spriteRenderer.sprite = MazeImageLoader.Instance.PipeT;
            zRotation = 270;
            break;
        }
            
        case CONNECTOR_T0: // T
        {
            m_spriteRenderer.sprite = MazeImageLoader.Instance.PipeT;
            zRotation = 180;
            break;
        }
            
        case CONNECTOR_X: // +
        {
            m_spriteRenderer.sprite = MazeImageLoader.Instance.PipeX;
            zRotation = 0;
            break;
        }
            
        default:
        {
            //m_tConnector.gameObject.SetActive (false);
            m_spriteRenderer.enabled = false;
            break;
        }
        }
        
        m_spriteRenderer.transform.eulerAngles = new Vector3 (0, 0, zRotation);
    }
}