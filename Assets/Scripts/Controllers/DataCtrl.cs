using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataCtrl : MonoBehaviour
{
    // Start is called before the first frame update\
    public static DataCtrl instance = null;
    public GameData data;
    public bool devmode;
    BinaryFormatter bf;
    string dataFilePath;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // this functions doesnt destroy the object on changing the scene 
            bf = new BinaryFormatter();
            dataFilePath = Application.persistentDataPath + "/game.dat";
           // Debug.Log(dataFilePath);
        }
        else
        {
            Destroy(gameObject); //this makes sure that only one object is formed
        }
    }
    public void RefreshData()
    {
        if (File.Exists(dataFilePath))
        {
            FileStream fs = new FileStream(dataFilePath, FileMode.Open);
            data = (GameData)bf.Deserialize(fs);
            fs.Close();
          //  Debug.Log("Data Refreshed");
        }
    }

    public void OnEnable()
    {
        CheckDb();
    }

    public void CheckDb()
    {
        if (!File.Exists(dataFilePath))
        {
#if UNITY_ANDROID
            CopyDB();
#endif
        }
        else
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                string desfile = System.IO.Path.Combine(Application.streamingAssetsPath, "game.dat");
                File.Delete(desfile);
                File.Copy(dataFilePath, desfile);
            }
            if (devmode)
            {
                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
                    File.Delete(dataFilePath);
                    CopyDB();
                }
            }
            RefreshData();
        }
       
    }

    void CopyDB()
    {
        string srcfile = System.IO.Path.Combine(Application.streamingAssetsPath, "game.dat");
        WWW downloader = new WWW(srcfile);

        while (!downloader.isDone)
        {

        }

        File.WriteAllBytes(dataFilePath, downloader.bytes);
        RefreshData();
    }
    public bool isUnlocked(int levelnumber)
    {
        return data.levelData[levelnumber].isUnlocked;
    }

    public int StarsAwarded(int levelnumber)
    {
        return data.levelData[levelnumber].starsAwarded;
    }
    public void SaveData()
    {
        FileStream fs = new FileStream(dataFilePath, FileMode.Create);
        bf.Serialize(fs, data);
        fs.Close();

       // Debug.Log("Data Saved");
    }

    public void SaveData(GameData data)
    {
        FileStream fs = new FileStream(dataFilePath, FileMode.Create);
        bf.Serialize(fs, data);
        fs.Close();

       // Debug.Log("Data Saved");
    }


}
