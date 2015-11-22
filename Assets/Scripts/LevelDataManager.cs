/*
 * developer     : brian g. tria
 * creation date : 2015.11.21
 *
 */

using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class LevelDataManager
{
    // TODO: Data Encryption
    private const string LEVELDATA_FILENAME = "/LevelData.json";

    private static LevelDataManager m_instance = null;
    private List<LevelData> m_listLevelData = new List<LevelData> ();
    
    public List<LevelData> ListLevelData {get {return m_listLevelData;}}
    public static LevelDataManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new LevelDataManager ();
                m_instance.Load ();
            }
            
            return m_instance;
        }
    }
    
    public bool Load ()
    {
        string strPath = Utilities.GetGameDataDirectory () + LEVELDATA_FILENAME;
        if (File.Exists (strPath) == false) { return false; }
        
        string strJson = File.ReadAllText (Utilities.GetGameDataDirectory () + LEVELDATA_FILENAME);
        if (string.IsNullOrEmpty (strJson)) { return false; }
        
        m_listLevelData = JsonMapper.ToObject<List<LevelData>> (strJson);
        return true;
        
//        string strLevelData = string.Empty;
//        for (int idx = 0; idx < m_listLevelData.Count; ++idx)
//        {
//            strLevelData += "\nID\t: " + m_listLevelData[idx].ID;
//            strLevelData += "\nWidth\t: " + m_listLevelData[idx].MazeWidth;
//            strLevelData += "\nHeight\t: " + m_listLevelData[idx].MazeHeight;
//            strLevelData += "\nMatrixSize\t: " + (m_listLevelData[idx].WallPlacementFlags.Length + 1);
//            strLevelData += "\n=====";
//        }
//        
//        Debug.Log (strLevelData + "\n");
    }

	public void Save (LevelData p_levelData)
    {
        p_levelData.ID = m_listLevelData.Count;
        m_listLevelData.Add (p_levelData);
        // overwrite .json file
        File.WriteAllText (Utilities.GetGameDataDirectory () + LEVELDATA_FILENAME, JsonMapper.ToJson (m_listLevelData));
    }
}

public class LevelData
{
    public int ID { get; set; }
    public int MazeWidth { get; set; }
    public int MazeHeight { get; set; }
    public int StartPointID { get; set; }
    public int EndPointID { get; set; }
    
    // TODO: convert to procedural level generation: just save the seed
    public List<RelativePosition> [] WallPlacementFlags { get; set; }
}