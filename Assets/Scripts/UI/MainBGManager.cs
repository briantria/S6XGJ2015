/*
 * developer     : brian g. tria
 * creation date : 2015.11.23
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainBGManager : MonoBehaviour
{
	[SerializeField] Transform m_bgTransform;
    
    private List<Transform> m_tBgTiles = new List<Transform> ();
    private Transform m_tMainCamera;
    private Transform m_transform;
    private bool m_bAllowDisplay;
    private Vector2 m_v2BgSize;
    private Vector2 m_v2PrevCameraPosition;
    private Vector2 m_v2CurrCameraPosition;
    private Vector2 m_v2CameraDistanceFromOrigin = Vector2.zero;
    
    // offsets and on actual position (except for the original bg position)
    private readonly Vector2[] bgPositionOffset = new Vector2[]
    {
        new Vector2 ( 0,  0),
        
        new Vector2 (-1,  1), new Vector2 (0,  1), new Vector2 (1,  1),
        new Vector2 (-1,  0),                      new Vector2 (1,  0),
        new Vector2 (-1, -1), new Vector2 (0, -1), new Vector2 (1, -1)
    };
    
    protected void Awake ()
    {
        m_transform = this.transform;
        m_tMainCamera = Camera.main.transform;
        m_bAllowDisplay = false;
        
        Sprite bgSprite = m_bgTransform.GetComponent<SpriteRenderer> ().sprite;
        m_v2BgSize = bgSprite.bounds.size * m_bgTransform.localScale.x; // bg scale must be uniform
        
        m_v2PrevCameraPosition = m_tMainCamera.position;
        m_v2CurrCameraPosition = m_tMainCamera.position;
        m_bgTransform.position = m_v2PrevCameraPosition;
        
        m_tBgTiles.Add (m_bgTransform);
        for (int idx = 1; idx < 9; ++idx)
        {
            m_tBgTiles.Add (Instantiate<Transform> (m_bgTransform));
            m_tBgTiles[idx].SetParent (m_transform);
            m_tBgTiles[idx].position = (Vector2) m_tBgTiles[idx].position + Vector2.Scale (m_v2BgSize, bgPositionOffset[idx]);
        }
    }
    
    protected void Update ()
    {
        if (!m_bAllowDisplay) { return; }
        
        m_v2CurrCameraPosition = m_tMainCamera.position;
        Vector2 camPosDelta = m_v2PrevCameraPosition - m_v2CurrCameraPosition;
        m_v2CameraDistanceFromOrigin += (camPosDelta * Time.deltaTime * 20);
        
        // TODO: something is fishy
        //       bg seems to ignore camera at some point
        //       maybe reset m_v2CameraDistanceFromOrigin? or think of other algo
        
        for (int idx = 0; idx < 9; ++idx)
        {
            Vector2 bgTilePos = m_v2CameraDistanceFromOrigin + Vector2.Scale (m_v2BgSize, bgPositionOffset[idx]);
            
            // adjust if bg tile is too far left
            if (m_v2CurrCameraPosition.x - bgTilePos.x > m_v2BgSize.x) 
            {
                bgTilePos.x += (m_v2BgSize.x * 3);
            }
            // adjust if bg tile is too far right
            else if (m_v2CurrCameraPosition.x - bgTilePos.x < -m_v2BgSize.x) 
            {
                bgTilePos.x -= (m_v2BgSize.x * 3);
            }
            
            // adjust if bg tile is too far down
            if (m_v2CurrCameraPosition.y - bgTilePos.y > m_v2BgSize.y) 
            {
                bgTilePos.y += (m_v2BgSize.y * 3);
            }
            // adjust if bg tile is too far up
            else if (m_v2CurrCameraPosition.y - bgTilePos.y < -m_v2BgSize.y) 
            {
                bgTilePos.y -= (m_v2BgSize.y * 3);
            }
            
            m_tBgTiles[idx].position = bgTilePos;
        }
        
        m_v2PrevCameraPosition = m_v2CurrCameraPosition;
    }
}