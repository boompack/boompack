using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MessagePack;
using System.IO;
using System.Diagnostics;

public class SaveManager : Singleton<SaveManager>
{

    public LevelStatsObject levelStats = new LevelStatsObject();
    // Start is called before the first frame update
    void Start()
    {
        //CreateFirstTime();
        LoadLevelStats();

    }

    public void CreateFirstTime()
    {
        foreach (var item in LevelManager.Instance.levelsDictionary)
        {
            levelStats.levelStatsDict.Add(item.Key, new LevelStat() { levelID = item.Key });
        }

        levelStats.levelStatsDict[1].isLocked = false;
        levelStats.levelStatsDict[1].isPlayable = true;


        for (int i = 2; i <= 10; i++)
        {
            levelStats.levelStatsDict[i].isLocked = false;
            levelStats.levelStatsDict[i].isPlayable = false;
            //levelStats.levelStatsDict[i].isPlayable = true;

        }
        //for (int i = 30; i <= 60; i++)
        //{
        //    levelStats.levelStatsDict[i].isPlayable = true;
        //}
        for (int i = 11; i <= 300; i++)
        {
            //levelStats.levelStatsDict[i].isPlayable = false;
            levelStats.levelStatsDict[i].isLocked = true;

        }

        UnityEngine.Debug.Log("Sayımız Bu: " + levelStats.levelStatsDict.Count);

        levelStats.lastOpenedLevel = 1;
        levelStats.lastOpenedState = 1;
        levelStats.lastPlayedLevel = 0;
        SaveLevelStats();
    }

    public void LoadLevelStats()
    {

        if(!ES3.FileExists("BoomPackUserData.txt"))
        {
            CreateFirstTime();
        }
        else
        {
            levelStats = MessagePackSerializer.Deserialize<LevelStatsObject>(ES3.LoadRawBytes("BoomPackUserData.txt"));
        }


/*
        Stopwatch saat = new Stopwatch();
        saat.Start();
#if UNITY_ANDROID
        UnityWebRequest www = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, "levelStats.txt"));
        www.SendWebRequest();
        while (!www.isDone) ;
        levelStats = MessagePackSerializer.Deserialize<LevelStatsObject>(www.downloadHandler.data);
        GamePackManager.Instance.UpdateAll();
        saat.Stop();
        UnityEngine.Debug.Log("Level Stats Yükleme Android Byte Geçen Süre: " + saat.ElapsedMilliseconds);
#else
        //TODO Check for other platforms like iOS
        levelStats = MessagePackSerializer.Deserialize<LevelStatsObject>(File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, "levelStats.txt")));
        GamePackManager.Instance.UpdateAll();
        saat.Stop();
        UnityEngine.Debug.Log("Level Stats Yükleme Byte Geçen Süre: " + saat.ElapsedMilliseconds);
        saat.Reset();
#endif

        /*
        #if UNITY_ANDROID
                string path2 = Path.Combine(Application.streamingAssetsPath, "levelsByte.txt");
                UnityWebRequest www2 = UnityWebRequest.Get(path2);
                www2.SendWebRequest();
                while (!www2.isDone) ;
                saat.Start();
                levels = MessagePackSerializer.Deserialize<Levels>(www2.downloadHandler.data);
                saat.Stop();

        #endif  
        */
    }

    public void SaveLevelStats()
    {

        Stopwatch saat = new Stopwatch();
        saat.Start();

        ES3.SaveRaw(MessagePackSerializer.Serialize(levelStats), "BoomPackUserData.txt");
        saat.Stop();
        UnityEngine.Debug.Log("Level Stats Kayıt Byte Geçen Süre: " + saat.ElapsedMilliseconds);
        /*
#if UNITY_ANDROID


        byte[] myData = MessagePackSerializer.Serialize(levelStats);

        UnityWebRequest www = UnityWebRequest.Delete(Path.Combine(Application.streamingAssetsPath, "levelStats.txt"));
        www.SendWebRequest();
        while (!www.isDone) ;

        www = UnityWebRequest.Put(Path.Combine(Application.streamingAssetsPath, "levelStats.txt"), myData);
        www.SendWebRequest();
        while (!www.isDone) ;


        saat.Stop();
        UnityEngine.Debug.Log("Level Stats Kayıt Android Byte Geçen Süre: " + saat.ElapsedMilliseconds);
#else
        //TODO Check for other platforms like iOS
        File.WriteAllBytes(Path.Combine(Application.streamingAssetsPath, "levelStats.txt"), MessagePackSerializer.Serialize(levelStats));
        saat.Stop();
        UnityEngine.Debug.Log("Level Stats Kayıt Byte Geçen Süre: " + saat.ElapsedMilliseconds);
        saat.Reset();
#endif

        /*
        #if UNITY_ANDROID
                string path2 = Path.Combine(Application.streamingAssetsPath, "levelsByte.txt");
                UnityWebRequest www2 = UnityWebRequest.Get(path2);
                www2.SendWebRequest();
                while (!www2.isDone) ;
                saat.Start();
                levels = MessagePackSerializer.Deserialize<Levels>(www2.downloadHandler.data);
                saat.Stop();

        #endif  
        */
    }


    public void SaveLevelStat(int levelID, int point, int bonusUseCount, int repeatCount,int timeCount)
    {
        levelStats.levelStatsDict[levelID].isLocked = false;
        levelStats.levelStatsDict[levelID].isPlayable = true;
        levelStats.levelStatsDict[levelID].isPlayed = true;

        byte newLevelStar;
        
        if(bonusUseCount != 0 )
        {
            newLevelStar = 1;
        }
        else if (repeatCount == 0 && timeCount > 0)
        {
            newLevelStar = 3;
        } else
        {
            newLevelStar = 2;
        }

        if(levelStats.levelStatsDict[levelID].maxPoint < point)
        {
            levelStats.levelStatsDict[levelID].maxPoint = point;
        }

        if(levelStats.levelStatsDict[levelID].levelStar < newLevelStar)
        {
            levelStats.levelStatsDict[levelID].levelStar = newLevelStar;
        }

        GamePackManager.Instance.UpdateLevel(levelID);

        if(levelStats.lastPlayedLevel < levelID)
        {
            levelStats.lastPlayedLevel = levelID;
        }

        if(levelStats.lastOpenedState < Yardimcilar.GetLevelState(levelID))
        {
            UnityEngine.Debug.Log("Kayıtlı State Daha Küçük");
            UnityEngine.Debug.Log(Yardimcilar.GetLevelState(levelID));
            levelStats.lastOpenedState = Yardimcilar.GetLevelState(levelID);
        }

        SaveLevelStats();
        GamePackManager.Instance.UpdateSliders();
    }

    public void OpenRewardedLevels()
    {
        levelStats.levelStatsDict[AdsManager.Instance.startLevel+1].isLocked = false;
        levelStats.levelStatsDict[AdsManager.Instance.startLevel+1].isPlayable = true;

        for (int i = AdsManager.Instance.startLevel+2; i <= AdsManager.Instance.startLevel + AdsManager.Instance.numberOfSectionsToOpen; i++)
        {
            levelStats.levelStatsDict[i].isLocked = false;
            levelStats.levelStatsDict[i].isPlayable = false;
        }
        //for (int i = 10; i <= 20; i++)
        //{
        //    levelStats.levelStatsDict[i].isLocked = false;
        //    //levelStats.levelStatsDict[i].isPlayable = true;

        //}
        SaveLevelStats();
        LoadLevelStats();
    }
}

[MessagePackObject]
public class LevelStat
{
    [Key(0)]
    public int levelID;
    [Key(1)]
    public bool isLocked = true;
    [Key(2)]
    public bool isPlayable = false;
    [Key(3)]
    public bool isPlayed = false;
    [Key(4)]
    public byte levelStar = 0;
    [Key(5)]
    public int maxPoint = 0;
}

[MessagePackObject]
public class LevelStatsObject
{
    [Key(0)]
    public Dictionary<int, LevelStat> levelStatsDict = new Dictionary<int, LevelStat>();
    [Key(1)]
    public int lastPlayedLevel = 0;
    [Key(2)]
    public int lastOpenedLevel = 1;
    [Key(3)]
    public int lastOpenedState = 1;
}
