using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using GameAnalyticsSDK;
using MessagePack;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{

    public Levels levels;
    public Level playedLevel;

    public Dictionary<int, Level> levelsDictionary = new Dictionary<int, Level>();
    // Start is called before the first frame update
    void Start()
    {
        LoadLevels();
        LevelsToDictionary();
    }


    public void LoadLevels()
    {

        if(!ES3.FileExists("BoomPackLevels.txt"))
        {
            Stopwatch saat = new Stopwatch();
            saat.Start();
            UnityEngine.Debug.Log("Bölümler Dosyası Yok. Streaming Assets içinden yaratılıyor...");
            LoadFromStreamingAssets();
            ES3.SaveRaw(MessagePackSerializer.Serialize(levels), "BoomPackLevels.txt");
            ES3.Save("levelVersion" ,0);
            saat.Stop();
            UnityEngine.Debug.Log("İlk Level Yaratma Byte Geçen Süre: " + saat.ElapsedMilliseconds);
        }
        else
        {
            Stopwatch saat = new Stopwatch();
            saat.Start();
            levels = MessagePackSerializer.Deserialize<Levels>(ES3.LoadRawBytes("BoomPackLevels.txt"));
            saat.Stop();
            UnityEngine.Debug.Log("Level Okuma ES3 Byte Geçen Süre: " + saat.ElapsedMilliseconds);
        }


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

    public void LoadLevel(int levelID)
    {
        //TODO
        playedLevel = levelsDictionary[levelID];
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, Yardimcilar.GetLevelState(levelID).ToString(), levelID.ToString());
    }

    public void LevelsToDictionary()
    {
        Stopwatch saat = new Stopwatch();
        saat.Start();
        foreach (var item in levels.levelsList)
        {
            levelsDictionary.Add(item.levelID, item);
        }
        saat.Stop();
        UnityEngine.Debug.Log("Level Ekleme For Each süresi: " + saat.ElapsedMilliseconds);
    }

    public void LoadTestLevel(int testLevelID)
    {
        playedLevel = levelsDictionary[testLevelID];
    }

    public void LoadLevelsAgain()
    {
        levels.levelsList.Clear();
        levels = null;
        levelsDictionary.Clear();

        LoadLevels();
        LevelsToDictionary();
    }

    public void LoadFromStreamingAssets()
    {

        Stopwatch saat = new Stopwatch();
        saat.Start();
#if UNITY_ANDROID
        UnityWebRequest www = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, "levelsByte.txt"));
        www.SendWebRequest();
        while (!www.isDone) ;
        levels = MessagePackSerializer.Deserialize<Levels>(www.downloadHandler.data);
        saat.Stop();
        UnityEngine.Debug.Log("Level Yükleme Android Byte Geçen Süre: " + saat.ElapsedMilliseconds);
#else
        //TODO Check for other platforms like iOS
        levels = MessagePackSerializer.Deserialize<Levels>(File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, "levelsByte.txt")));
        saat.Stop();
        UnityEngine.Debug.Log("Level Yükleme Byte Geçen Süre: " + saat.ElapsedMilliseconds);
        saat.Reset();
#endif
    }

    public void CheckOnlineLevels()
    {
        StartCoroutine(GetRequest("https://www.example.com"));

    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                UnityEngine.Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                UnityEngine.Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                if(ES3.Load("levelVersion", 0) < Int32.Parse(webRequest.downloadHandler.text))
                {
                ES3.Save("levelVersion",webRequest.downloadHandler.text );
                DownloadOnlineLevels();
                }
            }
        }
    }

    public void DownloadOnlineLevels()
    {
        StartCoroutine(DownloadRequest("https://www.example.com"));
    }

    IEnumerator DownloadRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                UnityEngine.Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                UnityEngine.Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                levels = MessagePackSerializer.Deserialize<Levels>(webRequest.downloadHandler.data);
                ES3.SaveRaw(webRequest.downloadHandler.data, "BoomPackLevels.txt");
            }
        }
    }
}
